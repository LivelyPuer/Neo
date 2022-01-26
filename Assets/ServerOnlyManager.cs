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

    private void Start()
    {
        _dataManager = FindObjectOfType<DataManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
       
    }
    
    
}
