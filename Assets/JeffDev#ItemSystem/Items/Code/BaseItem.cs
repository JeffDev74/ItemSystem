using FPS.InventorySystem;
using System;
using UnityEngine;
using ItemSystem;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public abstract class BaseItem : ICoreData
    {
        public BaseItem() { }

        public BaseItem(ISData _sData, INSData _nsData)
        {
            BaseData = _sData;
            BaseNSData = _nsData;
        }

        protected abstract void Init();

        public abstract ISData BaseData { get; set; }

        public abstract INSData BaseNSData { get; set; }

        public abstract IInventory Inventory { get; set; }

        public abstract IDBModel DBModel { get; set; }

        public abstract BaseItem FactoreNewItem(ISData data, INSData nsData);

        public virtual BaseItem ShallowCopy()
        {
            return this.MemberwiseClone() as BaseItem;
        }
    }
}