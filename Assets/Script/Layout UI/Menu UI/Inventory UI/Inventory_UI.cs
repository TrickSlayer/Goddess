using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

public class Inventory_UI : MonoBehaviour
{
    public List<Slot_UI> slots = new List<Slot_UI>();
    public GameObject detailPanel;
    [HideInInspector] public static Inventory_UI instance;
    [HideInInspector] PlayerInventory inventoryP;
    [HideInInspector] Inventory inventory;
    public InventoryDetail_UI inventoryDetail_UI;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        detailPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (inventoryP == null)
        {
            inventoryP = PlayerInventory.instance;
            inventory = inventoryP.inventory;
        }

        if (inventory.needFresh)
        {
            Refresh();

            if (inventory.newSlot == inventoryDetail_UI.inventorySlot)
            {
                inventory.newSlot = null;
                RefreshDetail(inventory.slots[inventoryDetail_UI.slotId]);
            }
        }
    }

    public void Refresh()
    {
        if (slots.Count == inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
        inventory.needFresh = false;
    }

    void RefreshDetail(Inventory.Slot slot)
    {
        if (detailPanel.activeSelf)
        {
            inventoryDetail_UI.SetItem(slot);
        }
    }

    private Item getItemDrop(int slotId)
    {
        GameObject objectItem = ObjectPooler.instance.GetGameObject(
            inventory.slots[slotId].itemName);

        Item itemToDrop = objectItem.GetComponent<Item>();

        return itemToDrop;
    }

    public void Remove(int slotId)
    {
        Item itemToDrop = getItemDrop(slotId);

        if (itemToDrop != null)
        {
            inventoryP.DropItem(itemToDrop);
            inventory.Remove(slotId);
            Refresh();
            if (slots[slotId].quantityText.text == "")
            {
                inventoryDetail_UI.inventorySlot = null;
                detailPanel.SetActive(false);
            }
            RefreshDetail(inventory.slots[slotId]);
        }

    }

    public void RemoveAll(int slotId)
    {
        Item itemToDrop = getItemDrop(slotId);

        if (itemToDrop != null)
        {
            int quantity = Int32.Parse(slots[slotId].quantityText.text);
            while (quantity-- > 0)
            {
                inventoryP.DropItem(itemToDrop);
                inventory.Remove(slotId);
            }
            Refresh();
            inventoryDetail_UI.inventorySlot = null;
            detailPanel.SetActive(false);
            RefreshDetail(inventory.slots[slotId]);
        }

    }

    public void Selected(int slotId)
    {
        Inventory.Slot slot = inventory.slots[slotId];

        if (slot == inventoryDetail_UI.inventorySlot)
        {
            inventoryDetail_UI.inventorySlot = null;
        }
        else
        {
            inventoryDetail_UI.inventorySlot = slot;
        }

        if (inventoryDetail_UI.inventorySlot == null)
        {
            detailPanel.SetActive(false);
            return;
        }

        if (inventoryDetail_UI.SetItem(slot))
        {
            detailPanel.SetActive(true);
        }

    }

    public void UseItem(int slotId)
    {
        Item itemToDrop = getItemDrop(slotId);

        if (itemToDrop != null)
        {
            inventory.Remove(slotId);
            Refresh();
            if (slots[slotId].quantityText.text == "")
            {
                inventoryDetail_UI.inventorySlot = null;
                detailPanel.SetActive(false);
            }
            RefreshDetail(inventory.slots[slotId]);
        }
    }

}
