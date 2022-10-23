using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyInformation_UI : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Health;

    GameObject Selected = null;

    private void Start()
    {
        Fresh();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject mark = GameObject.FindGameObjectWithTag("Mark");
        if(mark != null)
        {
            GameObject newSelected = mark.transform.parent.gameObject;
            if (newSelected.tag.Equals("Enemy"))
            {
                if (Selected == null)
                {
                    Selected = newSelected;
                } else
                {
                    if(Selected.GetInstanceID() == newSelected.GetInstanceID())
                    {
                        Selected = newSelected;
                        Enemy enemy = Selected.GetComponent<Enemy>();
                        Name.text = enemy.data.enemyName;
                        Health.text = enemy.data.currentHealth + "/" + enemy.data.Health.Value;
                    }

                }

            }
        } else
        {
            Fresh();
        }

    }

    void Fresh()
    {
        Name.text = "";
        Health.text = "";
    }
}
