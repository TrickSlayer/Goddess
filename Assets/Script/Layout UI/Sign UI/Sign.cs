using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI text;
    public string dialog;
    [HideInInspector] public bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            } else
            {
                dialogBox.SetActive(true);
                text.text = dialog;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
        Debug.Log(inRange);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
        Debug.Log(inRange);
    }
}
