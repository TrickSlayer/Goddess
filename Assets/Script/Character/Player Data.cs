using Goddess.PlayerStat;
using Newtonsoft.Json;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public CharacterStat Health;
    public CharacterStat Mana;
    public CharacterStat Defense;
    public CharacterStat Attack;
    public CharacterStat CritRate;
    public CharacterStat CritDamage;
    public CharacterStat Dodge;
    public int currentHealth;
    public int currentMana;

    List<SlotTexture> inventorySlotTexture;

    public float[] position;

    public PlayerData(PlayerStats statsP, PlayerInventory inventoryP, PlayerMovement movementP)
    {

        Health = statsP.Health; Mana = statsP.Mana; Defense = statsP.Defense;
        Attack = statsP.Attack; CritRate = statsP.CritRate; CritDamage = statsP.CritDamage;
        Dodge = statsP.Dodge;
        currentHealth = statsP.currentHealth; currentMana = statsP.currentMana;

        inventorySlotTexture = getTextSlots(inventoryP.inventory.slots);

        position = new float[3];
        position[0] = movementP.transform.position.x;
        position[1] = movementP.transform.position.y;
        position[2] = movementP.transform.position.z;
    }

    private List<SlotTexture> getTextSlots(List<Inventory.Slot> slots)
    {
        List<SlotTexture> textSlots = new List<SlotTexture>();
        foreach(var slot in slots)
        {
            SlotTexture textSlot = new SlotTexture();
            textSlot.Serialize(slot);
            textSlots.Add(textSlot);
        }

        return textSlots;
    }

    public List<Inventory.Slot> getSlots()
    {
        List<Inventory.Slot> slots = new List<Inventory.Slot>();
        foreach(var slotTexture in inventorySlotTexture)
        {
            slots.Add(slotTexture.DeSerialize());
        }

        return slots;
    }

    [Serializable]
    class SlotTexture
    {
        public string itemName;
        public int count;
        public int maxAllowed;

        public string icon = "";

        [ContextMenu("serialize")]
        public void Serialize(Inventory.Slot slot)
        {
            if (slot.icon != null)
            {
                SerializeTexture exportObj = new SerializeTexture();
                Texture2D tex = slot.icon.texture;
                exportObj.x = tex.width;
                exportObj.y = tex.height;
                exportObj.bytes = ImageConversion.EncodeToPNG(tex);
                icon = JsonConvert.SerializeObject(exportObj);
            }

            itemName = slot.itemName;
            count = slot.count;
            maxAllowed = slot.maxAllowed;
        }

        [ContextMenu("deserialize")]
        public Inventory.Slot DeSerialize()
        {
            Inventory.Slot slot = new Inventory.Slot();
            slot.itemName = itemName;
            slot.count = count;
            slot.maxAllowed = maxAllowed;

            if (icon != "")
            {
                SerializeTexture importObj = new SerializeTexture();
                importObj = JsonConvert.DeserializeObject<SerializeTexture>(icon);
                Texture2D tex = new Texture2D(importObj.x, importObj.y);
                ImageConversion.LoadImage(tex, importObj.bytes);
                Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
                slot.icon = mySprite;
            } else
            {
                slot.icon = null;
            }

            return slot;
        }
    }

    [Serializable]
    public class SerializeTexture
    {
        [SerializeField]
        public int x;
        [SerializeField]
        public int y;
        [SerializeField]
        public byte[] bytes;
    }
}
