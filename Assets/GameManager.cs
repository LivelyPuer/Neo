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
    public int defaultTime = 5;

    public SyncDictionary<uint, bool> playerRightID = new SyncDictionary<uint, bool>();

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

    public void AddPlayer(uint playerController)
    {
        if (!playerRightID.ContainsKey(playerController))
        {
            playerRightID.Add(playerController, false);
        }
       
    }

    public void SetDict(uint player, bool check)
    {
        playerRightID[player] = check;
    }
}