using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI manaText;
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetMaxMana(int mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
        manaText.text = slider.value + "/" + slider.maxValue;
    }

    public void SetMana(int mana)
    {
        slider.value = mana;
        manaText.text = slider.value + "/" + slider.maxValue;
    }
}
