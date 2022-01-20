using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RoomManager : NetworkBehaviour
{
    public List<uint> players;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(uint player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }
}
