using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI Text;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetMaxValue(int health)
    {
        slider.maxValue = health;
        Text.text = slider.value + "/" + slider.maxValue;
    }

    public void SetValue(int health)
    {
        slider.value = health;
        Text.text = slider.value + "/" + slider.maxValue;
    }
}
