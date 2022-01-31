using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPreview : NetworkBehaviour
{
    private NetworkRoomManager _lobbyManager;
    public List<Sprite> sprites;

    public Dictionary<NetworkRoomPlayer, PreviewRoomPlayer> previews =
        new Dictionary<NetworkRoomPlayer, PreviewRoomPlayer>();

    public Transform parentSlots;
    public GameObject playerSlot;
    public int oldCountPlayers = 0;

    void Start()
    {
        _lobbyManager = FindObjectOfType<NetworkRoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateRetry();
    }

    void GenerateRetry()
    {
        foreach (Transform child in parentSlots.transform)
        {
            Destroy(child.gameObject);
        }

        previews.Clear();
        foreach (NetworkRoomPlayer roomPlayer in _lobbyManager.roomSlots)
        {
            GameObject slot = Instantiate(playerSlot, Vector3.zero, Quaternion.identity, parentSlots);
            slot.GetComponent<Image>().sprite = sprites[roomPlayer.GetComponent<RoomPlayer>().current];
            slot.GetComponent<PreviewRoomPlayer>().readyText.text =
                roomPlayer.readyToBegin ? "<color=green>Готов" : "<color=red>Не готов";
            slot.GetComponent<PreviewRoomPlayer>().nameText.text = roomPlayer.isLocalPlayer ? "YOU" : "";
            previews.Add(roomPlayer, slot.GetComponent<PreviewRoomPlayer>());
        }

        oldCountPlayers = _lobbyManager.roomSlots.Count;
    }

    public void SetSkin(int i)
    {
        NetworkClient.localPlayer.GetComponent<RoomPlayer>().CmdSetSkin(i);
    }

    public void SetReady(bool state)
    {
        foreach (NetworkRoomPlayer roomPlayer in _lobbyManager.roomSlots)
        {
            roomPlayer.CmdChangeReadyState(state);
        }

        allReady();
    }

    // public void SetName(TMP_InputField setName)
    // {
    //     foreach (NetworkRoomPlayer roomPlayer in _lobbyManager.roomSlots)
    //     {
    //         roomPlayer.GetComponent<RoomPlayer>().CmdSetName(setName.text);
    //     }
    // }

    public void allReady()
    {
        foreach (NetworkRoomPlayer roomPlayer in _lobbyManager.roomSlots)
        {
            if (!roomPlayer.readyToBegin)
            {
                return;
            }
        }

        _lobbyManager.allPlayersReady = true;
    }
}