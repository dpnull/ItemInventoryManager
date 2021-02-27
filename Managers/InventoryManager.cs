using ItemInventoryManager.GameObjects;
using ItemInventoryManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemInventoryManager.Managers
{
    public static class InventoryManager
    {
        public const int DEFAULT_INVENTORY_SIZE = 10;

        public static int GetUniqueId()
        {
            return SlotDatabase.GetUniqueId();
        }

        public static void AddSlot(ISlot slot)
        {
            if (!SlotDatabase.Slots.ContainsKey(slot.ObjectId))
            {
                SlotDatabase.Slots.Add(slot.ObjectId, slot);
            }
        }

        public static void AddToInventory(IItem item)
        {
            var freeSlot = SlotDatabase.Slots.FirstOrDefault(f => !f.Value.Item.Any());

            if (item.IsUnique)
            {
                SlotDatabase.Slots[freeSlot.Key].Item.Add(item);
            }
            else
            {
                if (SlotDatabase.Slots.Values.Any(i => i.Item.Any(j => j.Name == item.Name)))
                {
                    foreach (var slot in SlotDatabase.Slots)
                    {
                        if (slot.Value.Item.Any(i => i.Name == item.Name))
                        {
                            slot.Value.Item.Add(item);
                        }
                    }
                } else
                {
                    if (freeSlot.Value.Item.All(i => i.Name != item.Name))
                    {
                        SlotDatabase.Slots[freeSlot.Key].Item.Add(item);
                    }
                }
            }
        }

        public static void CreateDefault()
        {
            while(SlotDatabase.Slots.Count < DEFAULT_INVENTORY_SIZE)
            {
                AddSlot(new Slot());
            }
        }

        public static T[] GetSlots<T>(Func<T, bool> criteria = null) where T : ISlot
        {
            var collection = SlotDatabase.Slots.Values.ToArray().OfType<T>();
            if (criteria != null)
            {
                collection = collection.Where(criteria.Invoke);
            }
            

            return collection.ToArray();
        }

        public static T GetSlot<T>(int objectId) where T : ISlot
        {
            var collection = SlotDatabase.Slots.ToArray();
            foreach(var item in collection)
            {
                return (T)SlotDatabase.Slots.Values.SingleOrDefault(i => i.ObjectId == objectId);
            }

            return default;
        }

        public static void RemoveSlotItem(int objectId)
        {
            // Not sure if necessary
            // ItemManager.Remove(SlotDatabase.Slots.GetValueOrDefault(objectId).Item);

            var removable = SlotDatabase.Slots[objectId].Item.First();
            SlotDatabase.Slots[objectId].Item.Remove(removable);
        }

        private static class SlotDatabase
        {
            public static readonly Dictionary<int, ISlot> Slots = new Dictionary<int, ISlot>();

            private static int _currentId;
            public static int GetUniqueId()
            {
                return _currentId++;
            }
        }
    }
}
