using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject dialog;
    public Master master;

    // Start is called before the first frame update
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

        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {
        GameObject masterObj = GameObject.FindGameObjectWithTag("Revival");
        master = masterObj.GetComponent<Master>();
    }

    private void Update()
    {
        if (GameManager.instance.currentScene.Equals("Beach")) {
            if (master == null)
            {
                GameObject masterObj = GameObject.FindGameObjectWithTag("Revival");
                master = masterObj.GetComponent<Master>();
            }

            if (!PlayerManager.instance.statsP.wasDie)
            {
                if (master.inRange && Input.GetKeyDown(KeyCode.F))
                {
                    if (dialog.activeInHierarchy)
                    {
                        dialog.SetActive(false);
                    }
                    else
                    {
                        dialog.SetActive(true);
                    }
                }
                else
            if (dialog.activeInHierarchy && !master.inRange)
                {
                    dialog.SetActive(false);
                }
            }
        }
    }

}
