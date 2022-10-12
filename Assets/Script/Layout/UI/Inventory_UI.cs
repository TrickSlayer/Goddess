using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public PlayerInventory playerInventory;
    public List<Slot_UI> slots = new List<Slot_UI>();
    public GameObject detailPanel;
    private InventoryDetail_UI detail;

    private void Awake()
    {
        inventoryPanel.SetActive(false);
        detailPanel.SetActive(false);
        detail = detailPanel.GetComponent<InventoryDetail_UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
        if (playerInventory.inventory.needFresh)
        {
            Refresh();

            if (playerInventory.inventory.newSlot == detail.inventorySlot)
            {
                playerInventory.inventory.newSlot = null;
                RefreshDetail(playerInventory.inventory.slots[detail.slotId]);
            }
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
        if (slots.Count == playerInventory.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
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
        playerInventory.inventory.needFresh = false;
    }

    void RefreshDetail(Inventory.Slot slot)
    {
        if (detailPanel.activeSelf)
        {
            detail.SetItem(slot);
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
            if (slots[slotId].quantityText.text == "")
            {
                detail.inventorySlot = null;
                detailPanel.SetActive(false);
            }
            RefreshDetail(playerInventory.inventory.slots[slotId]);
        }

    }

    public void Selected(int slotId)
    {
        Inventory.Slot slot = playerInventory.inventory.slots[slotId];

        if (slot == detail.inventorySlot)
        {
            detail.inventorySlot = null;
        }
        else
        {
            detail.inventorySlot = slot;
        }

        if (detail.inventorySlot == null)
        {
            detailPanel.SetActive(false);
            return;
        }

        detail.SetItem(slot);
        detailPanel.SetActive(true);

    }

    public void RemoveAll(int slotId)
    {

        Item itemToDrop = GameManager.instance.itemManager
            .GetItemByName(playerInventory.inventory.slots[slotId].itemName);


        if (itemToDrop != null)
        {
            int quantity = Int32.Parse(slots[slotId].quantityText.text);
            while (quantity-- > 0)
            {
                playerInventory.DropItem(itemToDrop);
                playerInventory.inventory.Remove(slotId);
            }
            Refresh();
            detail.inventorySlot = null;
            detailPanel.SetActive(false);
            RefreshDetail(playerInventory.inventory.slots[slotId]);
        }

    }

    public void UseItem(int slotId)
    {
        Item itemToDrop = GameManager.instance.itemManager
            .GetItemByName(playerInventory.inventory.slots[slotId].itemName);


        if (itemToDrop != null)
        {
            playerInventory.inventory.Remove(slotId);
            Refresh();
            if (slots[slotId].quantityText.text == "")
            {
                detail.inventorySlot = null;
                detailPanel.SetActive(false);
            }
            RefreshDetail(playerInventory.inventory.slots[slotId]);
        }
    }

}
