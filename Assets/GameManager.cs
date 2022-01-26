using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using Mirror;
using TurnTheGameOn.Timer;

public class GameManager : NetworkBehaviour
{
    private DataManager _dataManager;
    private Timer timer;
    public uint localPlayerID;
    [SyncVar] public int oldCount = 0;
    public int defaultTime = 5;
    public SyncDictionary<uint, bool> playerRightID = new SyncDictionary<uint, bool>();
    public SyncDictionary<uint, Question> playerQuestion = new SyncDictionary<uint, Question>();


    // Update is called once per frame
    private void Start()
    {
        _dataManager = FindObjectOfType<DataManager>();
        timer = FindObjectOfType<Timer>();
    }

    public void StartTimer(int seconds)
    {
        timer.SetTimerValue(seconds);
        timer.StartTimer();
    }

    public void StartTimer()
    {
        timer.SetTimerValue(defaultTime);
        timer.StartTimer();
    }

    [Server]
    private void Update()
    {
        if (FindObjectsOfType<PlayerController>().Length != oldCount)
        {
            oldCount = FindObjectsOfType<PlayerController>().Length;
            foreach (PlayerController player in FindObjectsOfType<PlayerController>())
            {
                CmdAddPlayer(player.GetComponent<NetworkIdentity>().netId);
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdAddPlayer(uint playerController)
    {
        if (!playerRightID.ContainsKey(playerController))
        {
            playerRightID.Add(playerController, false);
            playerQuestion.Add(playerController, null);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSetDict(uint player, bool check)
    {
        StartCoroutine(SetDictEnum(player, check));
    }

    [Server]
    IEnumerator SetDictEnum(uint player, bool check)
    {
        yield return null;
        playerRightID[player] = check;
    }

    // [TargetRpc]
    // private void SetDict(NetworkConnection conn, uint player, bool check)
    // {
    //     
    // }

    public bool GetPlayerAnswerDict(uint player)
    {
        return playerRightID[player];
    }
}