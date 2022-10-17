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

    private void Awake()
    {
        instance = this;
        detailPanel.SetActive(false);
        InventoryDetail_UI.instance = detailPanel.GetComponent<InventoryDetail_UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu_UI.instance.Show && PlayerInventory.instance.inventory.needFresh)
        {
            Refresh();

            if (PlayerInventory.instance.inventory.newSlot == InventoryDetail_UI.instance.inventorySlot)
            {
                PlayerInventory.instance.inventory.newSlot = null;
                RefreshDetail(PlayerInventory.instance.inventory.slots[InventoryDetail_UI.instance.slotId]);
            }
        }
    }

    void Refresh()
    {
        if (slots.Count == PlayerInventory.instance.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (PlayerInventory.instance.inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(PlayerInventory.instance.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
        PlayerInventory.instance.inventory.needFresh = false;
    }

    void RefreshDetail(Inventory.Slot slot)
    {
        if (detailPanel.activeSelf)
        {
            InventoryDetail_UI.instance.SetItem(slot);
        }
    }

    private Item getItemDrop(int slotId)
    {
        GameObject objectItem = ObjectPooler.instance.GetGameObject(
            PlayerInventory.instance.inventory.slots[slotId].itemName);

        Item itemToDrop = objectItem.GetComponent<Item>();

        return itemToDrop;
    }

    public void Remove(int slotId)
    {
        Item itemToDrop = getItemDrop(slotId);

        if (itemToDrop != null)
        {
            PlayerInventory.instance.DropItem(itemToDrop);
            PlayerInventory.instance.inventory.Remove(slotId);
            Refresh();
            if (slots[slotId].quantityText.text == "")
            {
                InventoryDetail_UI.instance.inventorySlot = null;
                detailPanel.SetActive(false);
            }
            RefreshDetail(PlayerInventory.instance.inventory.slots[slotId]);
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
                PlayerInventory.instance.DropItem(itemToDrop);
                PlayerInventory.instance.inventory.Remove(slotId);
            }
            Refresh();
            InventoryDetail_UI.instance.inventorySlot = null;
            detailPanel.SetActive(false);
            RefreshDetail(PlayerInventory.instance.inventory.slots[slotId]);
        }

    }

    public void Selected(int slotId)
    {
        Inventory.Slot slot = PlayerInventory.instance.inventory.slots[slotId];

        if (slot == InventoryDetail_UI.instance.inventorySlot)
        {
            InventoryDetail_UI.instance.inventorySlot = null;
        }
        else
        {
            InventoryDetail_UI.instance.inventorySlot = slot;
        }

        if (InventoryDetail_UI.instance.inventorySlot == null)
        {
            detailPanel.SetActive(false);
            return;
        }

        if (InventoryDetail_UI.instance.SetItem(slot))
        {
            detailPanel.SetActive(true);
        }

    }

    public void UseItem(int slotId)
    {
        Item itemToDrop = getItemDrop(slotId);

        if (itemToDrop != null)
        {
            PlayerInventory.instance.inventory.Remove(slotId);
            Refresh();
            if (slots[slotId].quantityText.text == "")
            {
                InventoryDetail_UI.instance.inventorySlot = null;
                detailPanel.SetActive(false);
            }
            RefreshDetail(PlayerInventory.instance.inventory.slots[slotId]);
        }
    }

}
