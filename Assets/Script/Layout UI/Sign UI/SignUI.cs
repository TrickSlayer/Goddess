using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<string> dialog;
    int id = 0;

    // Start is called before the first frame update
    void Start()
    {
        text.text = dialog[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                id += 1;
                if (id == dialog.Count) id = 0;
                text.text = dialog[id];
                if (id == 0)
                {
                    gameObject.SetActive(false);
                }
            }

        }
    }


}
