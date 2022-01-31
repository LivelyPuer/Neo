using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Ares : NetworkBehaviour
{
    [SyncVar] public uint playerId;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            playerId = other.GetComponent<NetworkIdentity>().netId;
        }
    }
}
