using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mirror;

public class GameManager : MonoBehaviour
{
    private DataManager _dataManager;
    
    // Update is called once per frame
    private void Start()
    {
        _dataManager = FindObjectOfType<DataManager>();
    }

    void Update()
    {
        
    }
}
