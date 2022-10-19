using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [HideInInspector] public GameObject Player;
    public float range = 10f;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        float dist = Vector3.Distance(Player.transform.position, transform.position);

        if (dist < range)
        {
            GameObject newSelection = ObjectPooler.instance.SpawnFromPool(
                "Mark",
                transform.GetChild(0).gameObject.transform.position,
                Quaternion.identity
            );
            newSelection.transform.SetParent(transform);
        }

    }

    private void Update()
    {
        Transform mark = transform.Find("Mark(Clone)");
        if (mark != null)
        {
            Debug.Log("He he");
            float dist = Vector3.Distance(Player.transform.position, transform.position);

            if (dist > range)
            {
                GameObject newSelection = ObjectPooler.instance.SpawnFromPool(
                    "Mark",
                    Player.transform.position,
                    Quaternion.identity
                );
                newSelection.transform.SetParent(Player.transform);
                newSelection.SetActive(false);
            }
        }
    }

}
