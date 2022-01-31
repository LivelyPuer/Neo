using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewRoomPlayer : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text readyText;
    public Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
