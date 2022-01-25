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
        public GameObject skin;
        public Animator animator;
        public int damage;
        public int health;
    }

    //Make the private field of our PlayerStats struct visible in the Inspector
    //by applying [SerializeField] attribute to it
    [SerializeField] public PlayerStats[] skins;

    [SyncVar] public bool answered;
    [SyncVar] public int currentHealth;
    [SyncVar] public int current = 0;
    public EnemyController enemy;
    private Camera mainCamera;
    public ParticleSystem right;
    public ParticleSystem left;

    public TMP_Text Name;
    private DataManager _dataManager;
    private UiManager _uiManager;
    private GameManager _gameManager;

    public override void OnStartClient()
    {
        FindObjectOfType<RoomManager>().AddPlayer(netId);
        
    }

    void Start()
    {
        _dataManager = GetComponent<DataManager>();
        _uiManager = FindObjectOfType<UiManager>();
        _gameManager = FindObjectOfType<GameManager>();
        if (isLocalPlayer)
        {
            Name.text = "YOU";
        }
        
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
    public void PlayParticle()
    {
        if (_gameManager.playerRightID[netId])
        {
            right.Play();
        }
        else
        {
            left.Play();
        }
    }

    void Attack()
    {
        skins[current].animator.SetTrigger("Attack");
        enemy.MinusHealth(skins[current].damage);
    }

    public void CheckAnswer(string answer)
    {
        _gameManager.SetDict(netId, _dataManager.CheckAnswer(answer));
    }

    public void ShowQuestion()
    {
        print(name + " " + isLocalPlayer);
        if (isLocalPlayer)
        {
            _gameManager.SetDict(netId, false);
            print("Call");
            _dataManager.GenerateNewQuestion();
            Question q = _dataManager.GetCurQuestion();
            print(q.title + "; " + q.right + "; " + q.a2 + "; " + q.a3 + "; " + q.a4);
            _uiManager.ShowBase();
            print(_uiManager.BasePanel.activeSelf);
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
            }

            _uiManager.timerAnswer.RestartTimer();
        }
    }
}