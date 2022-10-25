using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerWeapon : MonoBehaviour
{
    public Transform firePoint;
    ObjectPooler pooler;
    PlayerManager manager;
    PlayerStats PlayerStats;
    GameObject Selected = null;

    int skill1Mana = 10;
    int skill2Mana = 20;

    private void Start()
    {
        pooler = ObjectPooler.instance;
        firePoint = transform;
        manager = PlayerManager.instance;
        PlayerStats = manager.statsP;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.currentMana > skill1Mana)
            if (Input.GetKeyDown(KeyCode.E))
            {
                Shot();
                PlayerStats.UseSkill(skill1Mana);
            }

        if (PlayerStats.currentMana > skill2Mana)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject mark = GameObject.FindGameObjectWithTag("Mark");
                if (mark != null)
                {
                    GameObject newSelected = mark.transform.parent.gameObject;
                    if (newSelected.tag.Equals("Enemy"))
                    {
                        Debug.Log(newSelected + " ... " + Selected);
                        if (Selected == null || Selected.GetInstanceID() != newSelected.GetInstanceID())
                        {
                            Selected = newSelected;
                        }

                    }

                    Shot(Selected);
                    PlayerStats.UseSkill(skill2Mana);
                } else
                {
                    Selected = null;
                }

            }
    }

    void Shot(GameObject target = null)
    {
        GameObject bullet = pooler.SpawnFromPool("Bullet", firePoint.position, Quaternion.identity);
        Bullet bulletStat = bullet.GetComponent<Bullet>();
        bulletStat.target = target;
    }
}
