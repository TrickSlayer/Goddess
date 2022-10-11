using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;

    void Awake()
    {
        inventory = new Inventory(21);
    }

    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position;

        float randX = Random.Range(-1f, 1f);

        Vector3 spawnOffset = new Vector3(randX, 1f, 0f).normalized;

        Item droppedItem = Instantiate(
            item, 
            spawnLocation + 3 * spawnOffset, 
            Quaternion.identity
            );

        //droppedItem.Rigidbody2D.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }
}
