using FPS.InventorySystem;
using System;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public class AmmoItem : BaseItem
    {
        public AmmoItem()
        {
            BaseData.Type = ItemTypeEnum.Ammo;
        }

        [SerializeField]
        private AmmoSData _baseData;
        public override ISData BaseData
        {
            get
            {
                if(_baseData == null)
                {
                    _baseData = new AmmoSData();
                }
                return _baseData;
            }
            set { _baseData = value as AmmoSData; }
        }

        [SerializeField]
        private AmmoNSData _baseNSData;
        public override INSData BaseNSData
        {
            get
            {
                if(_baseNSData == null)
                {
                    _baseNSData = new AmmoNSData();
                }
                return _baseNSData;
            }
            set { _baseNSData = value as AmmoNSData; }
        }

        private IInventory _inventory;
        public override IInventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
    }
}