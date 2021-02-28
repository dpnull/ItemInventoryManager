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
        public static char input;

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
                Console.WriteLine("|1| Show inventory |2| Add an item to inv |3| equip an item");
                Console.WriteLine("|4| Show equipped  |5| Remove from inv    |6| upgrade weapon");

                ConsoleKeyInfo getInput = Console.ReadKey(true);
                input = getInput.KeyChar;

                Console.Clear();

                switch (input)
                {
                    case '1':
                        Player.Inventory.DrawInventory();
                        break;
                    case '2':
                        AddItemToInventory();
                        break;
                    case '3':
                        EquipItemFromInventory();
                        break;
                    case '4':
                        PrintEquippedWeapon();
                        break;
                    case '5':
                        RemoveItemFromInventory();
                        break;
                    case '6':
                        UpgradeWeapon();
                        break;
                    default:
                        break;
                }

            }
        }

        private void EquipItemFromInventory()
        {
            Console.WriteLine("Select item from inventory:");
            Player.Inventory.DrawInventory();
            ConsoleKeyInfo getInput = Console.ReadKey();
            input = getInput.KeyChar;

            Player.EquipWeapon(Int32.Parse(input.ToString()));
        }

        private void RemoveItemFromInventory()
        {
            Console.WriteLine("Select item from inventory:"); // THIS GOES NULL REMEMBER
            Player.Inventory.DrawInventory();
            ConsoleKeyInfo getInput = Console.ReadKey();
            input = getInput.KeyChar;

            InventoryManager.RemoveSlotItem(Player.Inventory.Bindable[Int32.Parse(input.ToString())].ObjectId);
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
            ConsoleKeyInfo getInput = Console.ReadKey();
            input = getInput.KeyChar;

            switch (input)
            {
                case '1':
                    InventoryManager.AddToInventory(WeaponItem.Axe());
                    break;
                case '2':
                    InventoryManager.AddToInventory(MiscItem.Quartz());
                    break;
                default:
                    break;
            }
        }
    }
}
