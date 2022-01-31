using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    private GameManager _gameManager;
    public TMP_Text answer;
    public string answerText;
    public TMP_Text a1;
    public TMP_Text a2;
    public TMP_Text a3;
    public TMP_Text a4;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SetNumberAnswer(string num)
    {
        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            player.CheckAnswerNumber(num);
        }
    }
    public void SetAnswer(int i)
    {
        switch (i)
        {
            case 1:
                answer.text = a1.text;
                answerText = a1.text;
                break;
            case 2:
                answer.text = a2.text;
                answerText = a2.text;
                break;
            case 3:
                answer.text = a3.text;
                answerText = a3.text;
                break;
            case 4:
                answer.text = a4.text;
                answerText = a4.text;
                break;
        }
        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            player.CheckAnswer(answerText);
        }
    }

    public void Answer()
    {
        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            player.PlayParticle();
        }
    }
}