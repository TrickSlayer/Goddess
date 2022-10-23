using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string dialog;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        text.text = dialog;
    }

    // Update is called once per frame
    void Update()
    {
    }


}
