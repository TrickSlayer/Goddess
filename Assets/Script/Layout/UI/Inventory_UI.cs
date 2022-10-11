using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public PlayerInventory playerInventory;
    public List<Slot_UI> slots = new List<Slot_UI>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Refresh();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    void Refresh()
    {
        if(slots.Count == playerInventory.inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if (playerInventory.inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(playerInventory.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int slotId)
    {
        Item itemToDrop = GameManager.instance.itemManager
            .GetItemByName(playerInventory.inventory.slots[slotId].itemName);

        if (itemToDrop != null)
        {
            playerInventory.DropItem(itemToDrop);
            playerInventory.inventory.Remove(slotId);
            Refresh();
        }


    }

}
