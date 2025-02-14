﻿using ItemInventoryManager.Interfaces;
using ItemInventoryManager.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemInventoryManager.GameObjects
{
    public class Slot : ISlot
    {
        public int ObjectId { get; set; }
        public List<IItem> Item { get; set; }

        public Slot()
        {
            Item = new List<IItem>();
            ObjectId = InventoryManager.GetUniqueId();
            InventoryManager.AddSlot(this);
        }

        // Probably deprecated
        public Slot(IItem item)
        {
            Item = new List<IItem>();
            ObjectId = InventoryManager.GetUniqueId();
            InventoryManager.AddSlot(this);

            Item.Add(item);
        }

    }
}
