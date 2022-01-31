using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private PlayerController _playerController;
    private EnemyController _enemyController;
    public Slider slider;
    public Gradient healthGradient;
    public Image fill;
    public bool isEnemy;

    void Start()
    {
        if (tag == "Enemy")
        {
            isEnemy = true;
            _enemyController = GetComponent<EnemyController>();
            SetMaxHealth(_enemyController.health);
        }
        else
        {
            _playerController = GetComponent<PlayerController>();
            SetMaxHealth(_playerController.health);
        }
    }

    void Update()
    {
        SetHealth(isEnemy ? _enemyController.currentHealth : _playerController.currentHealth);
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