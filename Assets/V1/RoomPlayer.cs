using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine.UI;

public class RoomPlayer : NetworkRoomPlayer
{
    public bool localPlayer;
    [SyncVar] public int current = 0;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        localPlayer = true;
    }

    // Update is called once per frame
    private void OnConnectedToServer()
    {
        print("connect");
        NetworkClient.localPlayer.GetComponent<PlayerController>().CmdSetCurrent(current);
    }


    [Command(requiresAuthority = false)]
    public void CmdSetSkin(int i)
    {
        SetSkin(i);
    }

    [Server]
    private void SetSkin(int i)
    {
        current = i;
    }
}