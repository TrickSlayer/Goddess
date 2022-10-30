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
        if (mark != null)
        {
            Transform newSelected = mark.transform.parent;
            if (newSelected == null)
            {
                mark.SetActive(false);
            }
            if (newSelected!= null && newSelected.gameObject.tag.Equals("Enemy"))
            {
                if (Selected == null || Selected.GetInstanceID() != newSelected.GetInstanceID())
                {
                    Selected = newSelected.gameObject;

                    Enemy enemy = Selected.GetComponent<EnemyFly>();
                    if (enemy == null)
                    {
                        enemy = Selected.GetComponent<GroundEnemyAI>();
                    }
                    if (enemy == null)
                    {
                        enemy = Selected.GetComponent<FrogEnemy>();
                    }

                    Name.text = enemy.data.enemyName;
                    Health.text = enemy.currentHealth + "/" + enemy.data.Health.Value;

                }
            }
        }
        else
        {
            Fresh();
            Selected = null;
        }

    }

    void Fresh()
    {
        Name.text = "";
        Health.text = "";
    }
}
