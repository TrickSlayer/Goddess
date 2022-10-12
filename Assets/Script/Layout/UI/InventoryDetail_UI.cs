using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using static Inventory;

public class InventoryDetail_UI : MonoBehaviour
{
    public Slot_UI slot;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public Inventory_UI inventoryUI;
    public PlayerStats player;

    [HideInInspector] public Inventory.Slot inventorySlot = null;
    [HideInInspector] public int slotId;
    private Item item;

    public void SetItem(Inventory.Slot inventorySlot)
    {
        this.inventorySlot = inventorySlot;

        item = GameManager.instance.itemManager
            .GetItemByName(inventorySlot.itemName);

        slot.SetItem(inventorySlot);

        Name.text = item.data.itemName;

        Description.text = item.data.description;

        slotId = findId();
    }

    private int findId()
    {
        for (int i = 0; i < inventoryUI.playerInventory.inventory.slots.Count; i++)
        {

            if (inventoryUI.playerInventory.inventory.slots[i] == inventorySlot)
            {
                return i;
            }
        }
        return -1;
    }

    public void DropItem()
    {
        if (slotId != -1)
            inventoryUI.Remove(slotId);
    }

    public void DropAllItem()
    {
        if (slotId != -1)
            inventoryUI.RemoveAll(slotId);
    }

    public void UseItem()
    {
        if (slotId != -1)
        {
            inventoryUI.UseItem(slotId);
            player.OnEquipmentChanged(item, null);
        }
    }
}
