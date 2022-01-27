using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    public string number;
    public int maxLen = 8;
    public TMP_Text numberText;

    public void Update()
    {
        numberText.text = number;
    }

    public void AddNumber(string i)
    {
        if (i == "delete" && i.Length >= 1)
        {
            string t_number = "";
            for (int j = 0; j < number.Length - 1; j++)
            {
                t_number += number[j];
            }

            number = t_number;
            return;
        }

        if (number.Length < maxLen)
        {
            number += i;
        }
    }
}