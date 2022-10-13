using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_UI : MonoBehaviour
{
    public GameObject backgroundPanel;
    [HideInInspector] public bool Show = false;

    public static Menu_UI instance;

    private void Awake()
    {
        instance = this;
        backgroundPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!backgroundPanel.activeSelf)
        {
            backgroundPanel.SetActive(true);
            Show = true;
        }
        else
        {
            backgroundPanel.SetActive(false);
            Show = false;
        }
    }
}
