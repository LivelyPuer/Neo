using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using TMPro;
using TurnTheGameOn.Timer;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Timer timerAnswer;
    public GameObject BasePanel;
    [Header("Simple")] public GameObject SimplePanel;
    public TMP_Text QuestionSimple;
    public TMP_Text a1;
    public TMP_Text a2;
    public TMP_Text a3;
    public TMP_Text a4;
    private GameManager _gameManager;
    [HideInInspector] public List<int> buttons = new List<int>() {0, 1, 2, 3};

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    

    public void ShowBase()
    {
        BasePanel.SetActive(true);
    }
    public void ShowSimplePanel()
    {
        SimplePanel.SetActive(true);
    }
    public void ShowQuestion()
    {
        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            player.ShowQuestion();
        }
    }

    public void CloseAll()
    {
        BasePanel.SetActive(false);
        _gameManager.StartTimer();
    }
}