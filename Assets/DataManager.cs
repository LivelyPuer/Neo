using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    List<Dictionary<string,object>> data;
    void Awake()
    {
        data = CSVReader.Read("questions");
    }

    private void Start()
    {
        print(data[0]["title"]);
    }

    public Question GetQuestions()
    {
        int index = Random.Range(0, data.Count);
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
}
