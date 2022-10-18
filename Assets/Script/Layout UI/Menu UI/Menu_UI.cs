using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu_UI : MonoBehaviour
{
    public GameObject backgroundPanel;
    public GameObject[] panel;
    [HideInInspector] public bool Show = false;

    public static Menu_UI instance;

    private void Awake()
    {
        instance = this;
        backgroundPanel.SetActive(false);
        OpenPanel(0);
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

    public void OpenPanel(int position)
    {
        for(int i = 0; i < panel.Length; i++)
        {
            if (i == position)
            {
                panel[i].gameObject.SetActive(true);
            } else
            {
                panel[i].gameObject.SetActive(false);
            }
        }
    }
}
