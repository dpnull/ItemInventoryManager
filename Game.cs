using ItemInventoryManager.GameObjects;
using ItemInventoryManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ItemInventoryManager.ItemTypes;
using ItemInventoryManager.Interfaces;
using ItemInventoryManager.Managers;

namespace ItemInventoryManager
{
    class Game
    {
        public static Player Player;

        public Game()
        {
            Player = new Player();

            InventoryManager.CreateDefault();

            ItemManager.Add(WeaponItem.None());
            // 0 reserved for no weapon
            Game.Player.CurrentWeapon = (WeaponItem)ItemManager.GetItem<IItem>(0);
        }

        public void GameLoop(bool c)
        {
            while (c)
            {
                Console.WriteLine("input:");
                Console.WriteLine("1) Show inventory slots");
                Console.WriteLine("2) Add item to inventory");
                Console.WriteLine("3) Equip an item from inventory");
                Console.WriteLine("4) Print currently equipped weapon");
                Console.WriteLine("5) Remove item from inventory");
                Console.WriteLine("6) Upgrade current weapon");
                string input = Console.ReadLine();
               
                if (input == "1")
                {
                    Player.Inventory.DrawInventory();
                }
                else if(input == "2")
                {
                    AddItemToInventory();
                }
                else if (input == "3")
                {
                    EquipItemFromInventory();
                }
                else if (input == "4")
                {
                    PrintEquippedWeapon();
                }
                else if (input == "5")
                {
                    RemoveItemFromInventory();
                }
                else if(input == "6")
                {
                    UpgradeWeapon();
                }
                else if (input == "c")
                {
                    Console.Clear();
                }
            }
        }

        private void EquipItemFromInventory()
        {
            Console.WriteLine("Select item from inventory:");
            Player.Inventory.DrawInventory();
            string input = Console.ReadLine();

            Player.EquipWeapon(Int32.Parse(input));
        }

        private void RemoveItemFromInventory()
        {
            Console.WriteLine("Select item from inventory:");
            Player.Inventory.DrawInventory();
            string input = Console.ReadLine();

            InventoryManager.RemoveSlotItem(Player.Inventory.Bindable[int.Parse(input)].ObjectId);
        }

        private void PrintEquippedWeapon()
        {
            var weapon = Player.CurrentWeapon;
            Console.WriteLine($"id: {weapon.ObjectId}    [{weapon.Name}]  [{weapon.MinDmg} - {weapon.MaxDmg}]");
        }

        private void UpgradeWeapon()
        {       
            Enchant.EnchantSteel(Player.CurrentWeapon);
            Console.WriteLine("Successfuly upgraded!");
        }

        private void AddItemToInventory()
        {
            Console.WriteLine("1) for weapon, 2) for misc");
            string input = Console.ReadLine();
            
            if (input == "1")
            {
                InventoryManager.AddToInventory(WeaponItem.Axe());
            } else if (input == "2")
            {
                InventoryManager.AddToInventory(MiscItem.Quartz());
            }
        }
    }
}
