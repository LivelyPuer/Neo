using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.Tanks;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Serializable]
    public struct PlayerStats
    {
        public GameObject skin;
        public Animator animator;
        public int damage;
        public int health;
    }

    //Make the private field of our PlayerStats struct visible in the Inspector
    //by applying [SerializeField] attribute to it
    [SerializeField]
    public PlayerStats[] skins;
    [SyncVar] public int currentHealth;
    [SyncVar] public int current = 0;
    public EnemyController enemy;
    private Camera mainCamera;

    public override void OnStartClient()
    {
        FindObjectOfType<RoomManager>().AddPlayer(netId);
    }

    void Start()
    {
        currentHealth = skins[current].health;
        mainCamera = Camera.main;
        enemy = FindObjectOfType<EnemyController>();
        if (current >= skins.Length)
        {
            current = 0;
        }

        foreach (PlayerStats skin in skins)
        {
            skin.skin.SetActive(false);
        }

        skins[current].skin.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        skins[current].animator.SetTrigger("Attack");
        enemy.MinusHealth(skins[current].damage);
    }
}