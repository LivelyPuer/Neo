using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    public Color firstColor = Color.white;
    public Color secondColor = Color.yellow;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        SetFirstColor();
    }

    public void SetFirstColor()
    {
        image.color = firstColor;
    }
    
    public void SetSecondColor()
    {
        image.color = Color.yellow;
    }

}
