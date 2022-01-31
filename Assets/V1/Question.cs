using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro.Examples;
using UnityEngine;


public class Question : ScriptableObject
{
    public enum Type{ Simple, Number, Date, Year };
    public string title;
    public Type typeQuestion;
    public string right;
    public string a2;
    public string a3;
    public string a4;
}
