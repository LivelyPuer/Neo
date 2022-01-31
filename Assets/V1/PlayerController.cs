using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Mirror.Examples.Tanks;
using TMPro;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Serializable]
    public struct PlayerStats
    {
        public Sprite skin;
        public Sprite skinDead;
    }

    //Make the private field of our PlayerStats struct visible in the Inspector
    //by applying [SerializeField] attribute to it
    [SerializeField] public PlayerStats[] skins;
    [SyncVar] public bool isRight;
    [SyncVar] public bool isDead;
    [SyncVar] public int currentHealth = 30;
    [SyncVar] public int current = 0;
    [SyncVar] public int damage = 50;
    public int health = 30;
    public EnemyController enemy;
    private Camera mainCamera;
    public ParticleSystem right;
    public ParticleSystem left;
    public TMP_Text Name;
    private DataManager _dataManager;
    private UiManager _uiManager;
    private GameManager _gameManager;
    private Health _health;
    public SpriteRenderer skin;

    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            FindObjectOfType<GameManager>().localPlayerID = netId;
        }
    }

    void Start()
    {
        _dataManager = GetComponent<DataManager>();
        _health = GetComponent<Health>();
        _uiManager = FindObjectOfType<UiManager>();
        _gameManager = FindObjectOfType<GameManager>();

        if (isLocalPlayer)
        {
            Name.text = "YOU";
        }


        currentHealth = health;
        mainCamera = Camera.main;
        enemy = FindObjectOfType<EnemyController>();
        if (current >= skins.Length)
        {
            current = 0;
        }

        skin.sprite = skins[current].skin;
    }

    [Command]
    public void CmdSetCurrent(int cur)
    {
        SetCurrent(cur);
    }

    [Server]
    private void SetCurrent(int cur)
    {
        current = cur;
    }

    private void Update()
    {
        if (hasAuthority) //проверяем, есть ли у нас права изменять этот объект
        {
            foreach (var v in FindObjectsOfType<RoomPlayer>())
            {
                if (v.localPlayer && hasAuthority)
                {
                    CmdSetCurrent(v.current);
                }
            }
        }
        skin.sprite = !isDead ? skins[current].skin : skins[current].skinDead;

    }

    private void LateUpdate()
    {
        if (currentHealth <= 0)
        {
            skin.sprite = skins[current].skinDead;
            isDead = true;
            if (hasAuthority)
            {
                _uiManager.ShowDead();
            }
        }
    }

    public void PlayParticle()
    {
        if (!isDead)
        {
            if (isRight)
            {
                right.Play();
                Attack();
            }
            else
            {
                MinusHealth(enemy.damage);
                left.Play();
            }
        }
    }


    void Attack()
    {
        if (hasAuthority)
        {
            print("Has " + hasAuthority);
            enemy.CmdChangeHealth(damage);
        }
    }

    public void MinusHealth(int count)
    {
        if (hasAuthority) //проверяем, есть ли у нас права изменять этот объект
        {
            if (isServer) //если мы являемся сервером, то переходим к непосредственному изменению переменной
                ChangeHealthValue(count);
            else
                CmdChangeHealth(count);
        }
    }

    [Server] //обозначаем, что этот метод будет вызываться и выполняться только на сервере
    public void ChangeHealthValue(int damage)
    {
        currentHealth -= damage;
    }

    [Command] //обозначаем, что этот метод должен будет выполняться на сервере по запросу клиента
    public void CmdChangeHealth(int damage) //обязательно ставим Cmd в начале названия метода
    {
        ChangeHealthValue(damage); //переходим к непосредственному изменению переменной
    }

    public void CheckAnswerNumber(string answer)
    {
        if (hasAuthority) //проверяем, есть ли у нас права изменять этот объект
        {
            if (isServer) //если мы являемся сервером, то переходим к непосредственному изменению переменной
                ChangeRightValue(_dataManager.CheckAnswer(answer));
            else
                CmdChangeRight(_dataManager.CheckAnswer(answer));
        }
    }

    public void CheckAnswer(string answer)
    {
        if (hasAuthority) //проверяем, есть ли у нас права изменять этот объект
        {
            if (isServer) //если мы являемся сервером, то переходим к непосредственному изменению переменной
                ChangeRightValue(_dataManager.CheckAnswer(answer));
            else
                CmdChangeRight(_dataManager.CheckAnswer(answer));
        }
    }

    [Server] //обозначаем, что этот метод будет вызываться и выполняться только на сервере
    public void ChangeRightValue(bool newValue)
    {
        isRight = newValue;
    }

    [Command] //обозначаем, что этот метод должен будет выполняться на сервере по запросу клиента
    public void CmdChangeRight(bool newValue) //обязательно ставим Cmd в начале названия метода
    {
        ChangeRightValue(newValue); //переходим к непосредственному изменению переменной
    }

    public void ShowQuestion()
    {
        if (!isDead)
        {
            if (hasAuthority) //проверяем, есть ли у нас права изменять этот объект
            {
                if (isServer) //если мы являемся сервером, то переходим к непосредственному изменению переменной
                    ChangeRightValue(false);
                else
                    CmdChangeRight(false);

                _dataManager.GenerateNewQuestion();
                Question q = _dataManager.GetCurQuestion();
                print(q.title + "; " + q.right + "; " + q.a2 + "; " + q.a3 + "; " + q.a4);
                _uiManager.ShowBase();
                switch (q.typeQuestion)
                {
                    case Question.Type.Simple:
                        _uiManager.ShowSimplePanel();
                        _uiManager.QuestionSimple.text = q.title;

                        List<string> answers = new List<string>() {q.right, q.a2, q.a3, q.a4};
                        System.Random rnd = new System.Random();
                        IOrderedEnumerable<int> randomized = _uiManager.buttons.OrderBy(item => rnd.Next());
                        int i = 0;
                        foreach (int index in randomized)
                        {
                            switch (i)
                            {
                                case 0:
                                    _uiManager.a1.text = answers[index];
                                    break;
                                case 1:
                                    _uiManager.a2.text = answers[index];
                                    break;
                                case 2:
                                    _uiManager.a3.text = answers[index];
                                    break;
                                case 3:
                                    _uiManager.a4.text = answers[index];
                                    break;
                            }

                            i++;
                        }

                        break;
                    case Question.Type.Number:
                        _uiManager.ShowNumberPanel();
                        _uiManager.InputFieldNumber.SetTextWithoutNotify("");
                        _uiManager.QuestionNumber.text = q.title;
                        break;
                }
            }
        }

        _uiManager.ResetTimer();
    }
}