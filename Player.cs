using System;
using System.Collections.Generic;
using System.Text;
using ItemInventoryManager.GameObjects;
using ItemInventoryManager.Interfaces;
using ItemInventoryManager.ItemTypes;
using ItemInventoryManager.Managers;
using System.Linq;

namespace ItemInventoryManager
{
    class Player
    {
        public Inventory Inventory { get; private set; }

        public WeaponItem CurrentWeapon { get; set; }

        public Player()
        {
            CurrentWeapon = (WeaponItem)ItemManager.GetItem<IItem>(0);
            Inventory = new Inventory();
        }


        public void EquipWeapon(int slotObjectId)
        {
            var slot = InventoryManager.GetSlot<ISlot>(slotObjectId);

            if(slot.Item != null)
            {
                var equippable = slot.Item.FirstOrDefault(i => i is WeaponItem);
                CurrentWeapon = (WeaponItem)equippable;
            }
        }

    }
}
