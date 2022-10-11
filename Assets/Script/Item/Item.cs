using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    protected CharacterStats player;

    public ItemData data;
    [HideInInspector] public Rigidbody2D Rigidbody2D;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        GameObject playerTag = GameObject.FindGameObjectWithTag("Player");
        player = playerTag.GetComponent<CharacterStats>();
    }
}

