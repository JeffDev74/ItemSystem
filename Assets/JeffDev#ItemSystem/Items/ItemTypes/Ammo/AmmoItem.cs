﻿using FPS.InventorySystem;
using System;
using UnityEngine;
using ItemSystem;

namespace FPS.ItemSystem
{

    [System.Serializable]
    public class AmmoItem : BaseItem, IStackable
    {
        public AmmoItem (ISData _data, INSData _nsData) : base(_data, _nsData)
        {
            BaseData = _data;
            BaseNSData = _nsData;
            Init();
        }

        public AmmoItem() : base()
        {
            Init();
        }

        protected override void Init()
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

        private IDBModel _dbModel;
        public override IDBModel DBModel
        {
            get
            {
                if (_dbModel == null)
                {
                    _dbModel = AmmoDBModel.LoadDb();
                }
                return _dbModel;
            }
            set { _dbModel = value; }
        }

        public override BaseItem FactoreNewItem(ISData data, INSData nsData)
        {
            AmmoItem newItem = new AmmoItem(data.SDeepClone() as ISData, nsData.NSDeepClone() as AmmoNSData);

            // In Case database default values get overriten, set them back here to default
            newItem.BaseData.UniqueUUID = System.Guid.NewGuid().ToString();
            newItem.BaseData.SlotID = -1;
            return newItem;
        }

        #region IStackable Interface Implementation

        public bool IsStackable
        {
            get { return (BaseData as IStackableData).IsStackable; }
            set { (BaseData as IStackableData).IsStackable = value; }
        }

        public bool DestroyOnUse
        {
            get { return (BaseData as IStackableData).DestroyOnUse; }
            set { (BaseData as IStackableData).DestroyOnUse = value; }
        }

        public int Quantity
        {
            get { return (BaseData as IStackableData).Quantity; }
            set { (BaseData as IStackableData).Quantity = value; }
        }

        public int StackableMax
        {
            get { return (BaseData as IStackableData).StackableMax; }
            set { (BaseData as IStackableData).StackableMax = value; }
        }

        public StackResult Stack(BaseItem rightItem)
        {
            // Istackabledata
            return null;
        }

        #endregion IStackable Interface Implementation
    }
}