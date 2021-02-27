using ItemInventoryManager.Interfaces;
using ItemInventoryManager.ItemTypes;
using System;
using System.Collections.Generic;
using System.Text;
using ItemInventoryManager.Managers;
using System.Linq;

namespace ItemInventoryManager.GameObjects
{
    public class Inventory
    {

        public List<ISlot> Bindable;
        public Inventory()
        {
        }

        public void DrawInventory()
        {
            var drawable = InventoryManager.GetSlots<ISlot>();
            var empty = new List<string>();

            var bindable = new List<ISlot>();

            foreach(var item in drawable)
            {
                if(!item.Item.Any())
                {
                    string emptyStr = "[EMPTY]";
                    empty.Add(emptyStr);
                    continue;
                }
                else
                {
                    if(item.Item.Any<IItem>(i => i.ObjectId == Game.Player.CurrentWeapon.ObjectId))
                    {
                        Console.WriteLine($"id: {item.Item.First().ObjectId}    [{item.Item.First().Name}]     - EQUIPPED -");
                        bindable.Add(item);
                    }
                    else if (item.Item.Any<IItem>(i => i.IsUnique))
                    {
                        Console.WriteLine($"id: {item.Item.First().ObjectId}    [{item.Item.First().Name}]");
                        bindable.Add(item);
                    }
                    else if(item.Item.Any<IItem>(i => !i.IsUnique))
                    {
                        Console.WriteLine($"{item.Item.First().Name}    Quantity: {item.Item.Count}");
                        bindable.Add(item);
                    }
                }
                Bindable = bindable;
            }

            if(empty.Count > 0)
            {
                foreach (var str in empty)
                {
                    Console.WriteLine(str);
                }
            }

        }
    }
}
