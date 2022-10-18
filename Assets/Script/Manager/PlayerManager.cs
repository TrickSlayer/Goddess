using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] PlayerInventory inventoryP;
    [HideInInspector] PlayerStats statsP;
    [HideInInspector] PlayerMovement movementP;
    [HideInInspector] GameObject player;
    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryP = player.GetComponent<PlayerInventory>();
        statsP = player.GetComponent<PlayerStats>();
        movementP = player.GetComponent<PlayerMovement>();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(statsP, inventoryP, movementP);
    }

    public void LoadPlayer()
    {
        PlayerData data =  SaveSystem.LoadPlayer();

        if(data == null)
        {
            return;
        }

        statsP.Health = data.Health;
        statsP.Mana = data.Mana;
        statsP.Attack = data.Attack;
        statsP.Defense = data.Defense;
        statsP.Dodge = data.Dodge;
        statsP.CritDamage = data.CritDamage;
        statsP.CritRate = data.CritRate;

        inventoryP.inventory.slots = data.getSlots();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        player.transform.position = position;

    }

    
}
