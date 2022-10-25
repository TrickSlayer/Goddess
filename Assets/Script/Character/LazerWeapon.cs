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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shot();
            PlayerStats.UseSkill(10);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject mark = GameObject.FindGameObjectWithTag("Mark");
            if (mark != null)
            {
                GameObject newSelected = mark.transform.parent.gameObject;
                if (newSelected.tag.Equals("Enemy"))
                {
                    if (Selected == null || Selected.GetInstanceID() == newSelected.GetInstanceID())
                    {
                        Selected = newSelected;
                    }

                }

                Shot(Selected);
                PlayerStats.UseSkill(20);
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
