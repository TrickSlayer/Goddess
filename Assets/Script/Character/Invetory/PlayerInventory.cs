using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;
    public static PlayerInventory instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        inventory = new Inventory(21);
    }


    public void DropItem(Item item)
    {

        Vector3 spawnLocation = transform.position;

        float randX = Random.Range(-1f, 1f);

        Vector3 spawnOffset = new Vector3(randX, 1f, 0f).normalized;

        ObjectPooler.instance.SpawnFromPool(
            item.data.name,
            spawnLocation + 3 * spawnOffset,
            Quaternion.identity
            );

    }
}
