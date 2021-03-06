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
    public TMP_Text spectatorText;
    public Timer timerAnswer;
    public GameObject BasePanel;
    [Header("Simple")] public GameObject SimplePanel;
    public TMP_Text QuestionSimple;
    public TMP_Text a1;
    public TMP_Text a2;
    public TMP_Text a3;
    public TMP_Text a4;
    [Header("Number")]
    public GameObject NumberPanel;
    public TMP_Text QuestionNumber;
    public TMP_InputField InputFieldNumber;
    
    
    
    
    private GameManager _gameManager;
    [HideInInspector] public List<int> buttons = new List<int>() {0, 1, 2, 3};

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void ResetTimer()
    {
        timerAnswer.RestartTimer();
    }

    public void ShowBase()
    {
        BasePanel.SetActive(true);
    }
    public void ShowSimplePanel()
    {
        SimplePanel.SetActive(true);
        NumberPanel.SetActive(false);
    }
    public void ShowNumberPanel()
    {
        SimplePanel.SetActive(false);
        NumberPanel.SetActive(true);
    }
    public void ShowQuestion()
    {
        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            player.ShowQuestion();
        }
    }

    public void ShowDead()
    {
        spectatorText.gameObject.SetActive(true);
    }

    public void CloseAll()
    {
        BasePanel.SetActive(false);
        _gameManager.StartTimer();
    }
}