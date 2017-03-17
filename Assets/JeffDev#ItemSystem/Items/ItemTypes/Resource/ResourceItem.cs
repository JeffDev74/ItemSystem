﻿using System;
using FPS.InventorySystem;
using UnityEngine;
using ItemSystem;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public class ResourceItem : BaseItem, IStackable
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

        private IDBModel _dbModel;
        public override IDBModel DBModel
        {
            get
            {
                if (_dbModel == null)
                {
                    _dbModel = ResourceDBModel.LoadDb();
                }
                return _dbModel;
            }
            set { _dbModel = value; }
        }

        public override BaseItem FactoreNewItem(ISData data, INSData nsData)
        {
            ResourceItem newItem = new ResourceItem(data.SDeepClone() as ISData, nsData.NSDeepClone() as ResourceNSData);

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

        public StackResult Stack(BaseItem itemToStack)
        {
            // NOTE:
            // By returning null in this method
            // a switch will happen on UISlot class

            itemToStack = itemToStack as ResourceItem;
            IStackableData iStackDataInterface = itemToStack.BaseData as IStackableData;
            if(iStackDataInterface == null)
            {
                return null;
            }

            if (itemToStack == null)
            {
                return null;
            }

            StackResult result = null;

            if (BaseData.Type == itemToStack.BaseData.Type)
            {
                if (IsStackable && iStackDataInterface.IsStackable)
                {
                    // check if the item in the left has available space to stack
                    if (StackableMax > Quantity)
                    {
                        result = new StackResult();

                        // how much can I stack
                        int canStackThisMuch = StackableMax - Quantity;

                        // now check if the item that was stacked on top 
                        // of the left item still have quantity
                        if (iStackDataInterface.Quantity > canStackThisMuch)
                        {
                            // the item has more than I can stack
                            Quantity += canStackThisMuch;
                            iStackDataInterface.Quantity -= canStackThisMuch;

                            //result.leftItem = left;
                            result.item = itemToStack;
                        }
                        else
                        {
                            // The item should be deleted will stack all its quantity
                            Quantity += iStackDataInterface.Quantity;
                            iStackDataInterface.Quantity = -1;
                            //result.leftItem = left;
                            result.item = itemToStack;
                        }
                        //Debug.Log("The items where stacked.");
                        return result;
                    }
                    else
                    {
                        //Debug.Log("The item in the left is full");
                        return null;
                    }
                }
                else
                {
                    //Debug.Log("one of the items is not stackable.");
                    return null;
                }
            }
            else
            {
                //Debug.Log("The item type is not the same");
                return null;
            }
        }

        #endregion IStackable Interface Implementation
    }
}