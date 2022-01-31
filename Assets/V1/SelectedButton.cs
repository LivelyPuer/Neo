using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    public Sprite firstColor;
    public Sprite secondColor;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        SetFirstColor();
    }

    public void SetFirstColor()
    {
        if (image != null)
        {
            image.sprite = firstColor;
        }
    }
    
    public void SetSecondColor()
    {
        if (image != null)
        {
            image.sprite = secondColor;
        }
    }

}
