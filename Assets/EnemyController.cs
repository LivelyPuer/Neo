using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnemyController : NetworkBehaviour
{
    [SyncVar] public int health = 1000;
    [SyncVar] public bool isDead;

    public void MinusHealth(int damage)
    {
        if (health <= 0) {return;}
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
        }
    }
}
