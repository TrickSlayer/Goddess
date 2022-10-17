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

    [HideInInspector] public Inventory.Slot inventorySlot = null;
    [HideInInspector] public int slotId;
    [HideInInspector] public static InventoryDetail_UI instance;
    private Item item;

    private void Awake()
    {
        instance = this;
    }

    public bool SetItem(Inventory.Slot inventorySlot)
    {
        this.inventorySlot = inventorySlot;

        GameObject objectItem = ObjectPooler.instance
            .GetGameObject(inventorySlot.itemName);

        if (objectItem == null)
        {
            return false;
        }

        item = objectItem.GetComponent<Item>();

        slot.SetItem(inventorySlot);

        Name.text = item.data.itemName;

        Description.text = item.data.description;

        slotId = findId();

        return true;
    }

    private int findId()
    {
        for (int i = 0; i < PlayerInventory.instance.inventory.slots.Count; i++)
        {

            if (PlayerInventory.instance.inventory.slots[i] == inventorySlot)
            {
                return i;
            }
        }
        return -1;
    }

    public void DropItem()
    {
        if (slotId != -1)
            Inventory_UI.instance.Remove(slotId);
    }

    public void DropAllItem()
    {
        if (slotId != -1)
            Inventory_UI.instance.RemoveAll(slotId);
    }

    public void UseItem()
    {
        if (slotId != -1)
        {
            Inventory_UI.instance.UseItem(slotId);
            PlayerStats.instance.OnEquipmentChanged(item, null);
        }
    }
}
