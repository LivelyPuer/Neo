using System.Collections.Generic;
using System.Collections;
using System;
using Mirror;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    List<Dictionary<string, object>> data;
    public Question currentQuestion;
    public bool alreadyGenerate;

    void Awake()
    {
        data = CSVReader.Read("questions");
        currentQuestion = GetQuestions();
    }

    private void Start()
    {
        print(data[0]["title"]);
    }

    public Question GenerateNewQuestion()
    {
        currentQuestion = GetQuestions();
        return currentQuestion;
    }

    public void OpenGenerate()
    {
        alreadyGenerate = false;
    }
    public Question GetCurQuestion()
    {
        return currentQuestion;
    }

    private Question GetQuestions()
    {
        int index = UnityEngine.Random.Range(0, data.Count);
        Question q = ScriptableObject.CreateInstance<Question>();
        q.title = data[index]["title"].ToString();
        switch (data[index]["type"].ToString())
        {
            case "number":
                q.typeQuestion = Question.Type.Number;
                break;
            case "date":
                q.typeQuestion = Question.Type.Date;
                break;
            case "year":
                q.typeQuestion = Question.Type.Year;
                break;
            default:
                q.typeQuestion = Question.Type.Simple;
                break;
        }

        q.right = data[index]["a1"].ToString();
        q.a2 = data[index]["a2"].ToString();
        q.a3 = data[index]["a3"].ToString();
        q.a4 = data[index]["a4"].ToString();
        return q;
    }

    public bool CheckAnswer(string answer)
    {
        Question q = GetCurQuestion();
        switch (q.typeQuestion)
        {
            case Question.Type.Simple:
                if (answer == q.right)
                    return true;
                return false;
            case Question.Type.Number:
                print(answer + "; " + q.right);
                if (answer == q.right)
                    return true;
                return false;
        }
        return false;
    }
}