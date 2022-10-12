using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI healthText;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthText.text = slider.value + "/" + slider.maxValue;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        healthText.text = slider.value + "/" + slider.maxValue;
    }
}
