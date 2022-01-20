using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private PlayerController _playerController;
    public Slider slider;
    public Gradient healthGradient;
    public Image fill;
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        SetMaxHealth(_playerController.skins[_playerController.current].health);
    }

    void Update()
    {
        SetHealth(_playerController.currentHealth);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = healthGradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = healthGradient.Evaluate(slider.normalizedValue);
    }
}
