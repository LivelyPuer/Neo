using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using TurnTheGameOn.Timer;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    private DataManager _dataManager;
    public GameObject EndPanel;
    public GameObject BadEndPanel;
    private ArtefactsManager.Artefact _artefact;
    public Image EndPanelImage;
    public TMP_Text EndPanelText;
    public List<Timer> timers = new List<Timer>();
    public EnemyController enemy;
    [SyncVar] public bool isStop;
    [SyncVar] public bool bossKilled;
    [SyncVar] public bool allDead;
    public bool giveArt;
    private Timer timer;
    public uint localPlayerID;

    [SyncVar] public int oldCount = 0;
    public int defaultTime = 5;


    // Update is called once per frame
    private void Start()
    {
        _artefact = FindObjectOfType<ArtefactsManager>().GetRandomArtefact();
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

    private void Update()
    {
        if (isStop)
        {
            foreach (var t in timers)
            {
                t.StopTimer();
            }
        }

        if (isServer)
        {
            allEnd();
        }

        if (isStop && !giveArt && bossKilled)
        {
            EndPanel.SetActive(true);
            FindObjectOfType<ArtefactsManager>().SetArtefact(_artefact);
            EndPanelImage.sprite = _artefact.sprite;
            EndPanelText.text = _artefact.title;
            giveArt = true;
        }else if (allDead)
        {
            BadEndPanel.SetActive(true);
        }
    }

    public void allEnd()
    {
        if (enemy.isDead)
        {
            isStop = true;
            if (!bossKilled)
            {
                bossKilled = true;
            }
        }
        else
        {
            allDead = true;
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

    public void Leave()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            FindObjectOfType<NetworkRoomManager>().StopHost();
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            FindObjectOfType<NetworkRoomManager>().StopClient();
        }
    }
}