using ItemInventoryManager.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemInventoryManager.Interfaces
{
    public interface ISlot
    {
        public int ObjectId { get; set; }
        public List<IItem> Item { get; set; }
    }
}
