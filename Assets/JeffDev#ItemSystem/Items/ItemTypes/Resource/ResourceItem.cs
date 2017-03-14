﻿using System;
using FPS.InventorySystem;
using UnityEngine;
using ItemSystem;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public class ResourceItem : BaseItem
    {
        public ResourceItem(ISData _data, INSData _nsData) : base(_data, _nsData)
        {
            BaseData = _data;
            BaseNSData = _nsData;
            Init();
        }

        public ResourceItem()
        {
            Init();
        }

        protected override void Init()
        {
            BaseData.Type = ItemTypeEnum.Resource;
        }

        [SerializeField]
        private ResourceSData _baseData;
        public override ISData BaseData
        {
            get
            {
                if(_baseData == null)
                {
                    _baseData = new ResourceSData();
                }
                return _baseData;
            }
            set { _baseData = value as ResourceSData; }
        }

        [SerializeField]
        private ResourceNSData _baseNSData;
        public override INSData BaseNSData
        {
            get
            {
                if(_baseNSData == null)
                {
                    _baseNSData = new ResourceNSData();
                }
                return _baseNSData;
            }
            set { _baseNSData = value as ResourceNSData; }
        }

        private IInventory _inventory;
        public override IInventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public override IDBModel DBModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override BaseItem FactoreNewItem(ISData data, INSData nsData)
        {
            throw new NotImplementedException();
        }
    }
}