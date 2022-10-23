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
    [HideInInspector] Inventory inventory;

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

    private void Start()
    {
        inventory = PlayerManager.instance.inventoryP.inventory;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.needFresh)
        {
            Refresh();

            if (inventory.newSlot == InventoryDetail_UI.instance.inventorySlot)
            {
                inventory.newSlot = null;
                RefreshDetail(inventory.slots[InventoryDetail_UI.instance.slotId]);
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
            InventoryDetail_UI.instance.SetItem(slot);
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
            PlayerInventory.instance.DropItem(itemToDrop);
            inventory.Remove(slotId);
            Refresh();
            if (slots[slotId].quantityText.text == "")
            {
                InventoryDetail_UI.instance.inventorySlot = null;
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
                PlayerInventory.instance.DropItem(itemToDrop);
                inventory.Remove(slotId);
            }
            Refresh();
            InventoryDetail_UI.instance.inventorySlot = null;
            detailPanel.SetActive(false);
            RefreshDetail(inventory.slots[slotId]);
        }

    }

    public void Selected(int slotId)
    {
        Inventory.Slot slot = inventory.slots[slotId];

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
            inventory.Remove(slotId);
            Refresh();
            if (slots[slotId].quantityText.text == "")
            {
                InventoryDetail_UI.instance.inventorySlot = null;
                detailPanel.SetActive(false);
            }
            RefreshDetail(inventory.slots[slotId]);
        }
    }

}
