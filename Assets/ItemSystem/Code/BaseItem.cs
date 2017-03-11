using FPS.InventorySystem;
using System;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public abstract class BaseItem : ICoreData
    {
        public abstract ISData BaseData { get; set; }

        public abstract INSData BaseNSData { get; set; }

        public abstract IInventory Inventory { get; set; }
    }
}