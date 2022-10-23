using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stat_UI : MonoBehaviour
{
    public TextMeshProUGUI Attribute;

    public void setAttribute(string text)
    {
        Attribute.text = text;
    }

}
