    using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Inventory;

[Serializable]
public class Inventory
{
    [Serializable]
    public class Slot
    {
        public string itemName;
        public int count;
        public int maxAllowed;

        public Sprite icon;

        public Slot()
        {
            itemName = "";
            count = 0;
            maxAllowed = 99;
        }

        public bool CanAddItem()
        {
            if(count < maxAllowed)
            {
                return true;
            }
            return false;
        }

        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.icon = item.data.icon;
            count++;
        }

        public void RemoveItem()
        {
            if(count > 0)
            {
                count--;

                if (count == 0)
                {
                    icon = null;
                    itemName = "";
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot> ();
    public bool needFresh = true;
    public Slot newSlot = null;

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot ();
            slots.Add(slot);
        }
    }

    public void Add(Item item)
    {
        needFresh = true;

        Slot s = slots.Where(s => s.itemName == item.data.itemName && s.CanAddItem()).FirstOrDefault();

        if (s != null)
        {
            s.AddItem(item);
            newSlot = s;
        } else
        {
            Slot e = slots.Where(s => s.itemName == "").FirstOrDefault();
            e.AddItem(item);
            newSlot = e;
        }

    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
}
