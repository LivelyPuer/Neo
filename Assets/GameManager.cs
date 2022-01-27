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
    
    public List<Timer> timers = new List<Timer>();
    public EnemyController enemy;
    [SyncVar] public bool isStop;
    
    
    private Timer timer;
    public uint localPlayerID;
    
    [SyncVar] public int oldCount = 0;
    public int defaultTime = 5;
    public SyncDictionary<uint, bool> playerRightID = new SyncDictionary<uint, bool>();
    public SyncDictionary<uint, Question> playerQuestion = new SyncDictionary<uint, Question>();


    // Update is called once per frame
    private void Start()
    {
        isStop = false;
        _dataManager = FindObjectOfType<DataManager>();
        timer = GetComponent<Timer>();
        enemy = FindObjectOfType<EnemyController>();
        foreach (var v in FindObjectsOfType<Timer>())
        {
            timers.Add(v);
        }
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
        if (isStop) {return;}
        allEnd();
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

    [Server]
    public void allEnd()
    {
        if (enemy.isDead)
        {
            isStop = true;
        }
        else
        {
            bool allDead = true;
            foreach (var player in FindObjectsOfType<PlayerController>())
            {
                if (!player.isDead)
                {
                    allDead = false;
                    break;
                }
            }

            isStop = allDead;
        }

        if (isStop)
        {
            foreach (var t in timers)
            {
                t.StopTimer();
            }
        }
        
    }
}