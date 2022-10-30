using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour
{
    [HideInInspector] public GameObject Player;
    public float range = 10f;
    private CircleCollider2D collider2D;

    private void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
    }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        Choose();
    }

    public void Choose()
    {
        float dist = Vector3.Distance(Player.transform.position, transform.position);

        if (dist < range)
        {
            Selected();
        }
    }

    public void Selected()
    {
       GameObject newSelection = ObjectPooler.instance.SpawnFromPool(
                "Mark",
                transform.GetChild(0).gameObject.transform.position,
                Quaternion.identity
            );
        newSelection.transform.SetParent(transform);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            if (collider2D.radius * transform.localScale.x >= Vector3.Distance(mousePosition, transform.position))
            {
                Choose();
            }

        }

        if (Player != null)
        {
            Transform mark = transform.Find("Mark(Clone)");
            if (mark != null)
            {
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
        } else
        {
            Player = GameObject.FindGameObjectWithTag("Player");

        }
    }

}
