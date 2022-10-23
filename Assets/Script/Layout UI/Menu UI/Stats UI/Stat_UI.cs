using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stat_UI : MonoBehaviour
{
    public TextMeshProUGUI Attribute;
    [HideInInspector] public static Stat_UI instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void setAttribute(string text)
    {
        Attribute.text = text;
    }

}
