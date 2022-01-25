using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TurnTheGameOn.Timer;
using UnityEngine;

public class ServerOnlyManager : NetworkBehaviour
{
    private DataManager _dataManager;
    private GameManager _gameManager;
    public int oldCount = 0;

    private void Start()
    {
        _dataManager = FindObjectOfType<DataManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (FindObjectsOfType<PlayerController>().Length != oldCount)
        {
            oldCount = FindObjectsOfType<PlayerController>().Length;
            foreach (PlayerController player in FindObjectsOfType<PlayerController>())
            {
                _gameManager.AddPlayer(player.GetComponent<NetworkIdentity>().netId);
            }
        }
        
    }
}
