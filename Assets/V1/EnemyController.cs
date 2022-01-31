using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnemyController : NetworkBehaviour
{
    [SyncVar] public int health = 1000;
    [SyncVar] public int currentHealth;
    [SyncVar] public int damage = 10;
    [SyncVar] public bool isDead;

    private void Start()
    {
        currentHealth = health;
    }
    
    [Server] //обозначаем, что этот метод будет вызываться и выполняться только на сервере
    public void ChangeHealthValue(int selfDamage)
    {
        if (currentHealth <= 0) {return;}
        currentHealth -= selfDamage;
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    [Command(requiresAuthority = false)] //обозначаем, что этот метод должен будет выполняться на сервере по запросу клиента
    public void CmdChangeHealth(int selfDamage) //обязательно ставим Cmd в начале названия метода
    {
        ChangeHealthValue(selfDamage); //переходим к непосредственному изменению переменной
    }
}
