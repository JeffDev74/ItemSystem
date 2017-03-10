﻿using UnityEngine;
using System.Collections.Generic;
using FPS.EventSystem;
using FPS.InventorySystem.Events;
using FPS.ItemSystem;

namespace FPS.InventorySystem
{
    public class Inventory : MonoBehaviour, IInventory
    {
        public int InventoryMaxItems = 10;

        private Transform _theTransform;
        public Transform TheTransform
        {
            get
            {
                if (_theTransform == null)
                {
                    _theTransform = transform;
                }
                return _theTransform;
            }
        }

        private List<IItem> _internalItems;
        private List<IItem> InternalItems
        {
            get
            {
                if (_internalItems == null)
                {
                    _internalItems = new List<IItem>();
                }
                return _internalItems;
            }
            set
            {
                _internalItems = value;
            }
        }

        //[SerializeField]
        public string _inventoryUUID;
        public string InventoryUUID
        {
            get
            {
                if (string.IsNullOrEmpty(_inventoryUUID))
                {
                    // Only used for testing
                    _inventoryUUID = "00071875-d6be-43c2-a254-d74f0893d000";
                    //_inventoryUUID = System.Guid.NewGuid().ToString();
                }
                return _inventoryUUID;
            }
            set { _inventoryUUID = value; }
        }

        public List<IItem> Items
        {
            get { return InternalItems; }
        }

        public int ItemsCount
        {
            get { return InternalItems.Count; }
        }

        protected virtual void OnEnable()
        {
            EventMessenger.Instance.AddListner<EventAddItemToInventory>(OnAddItemEvent);
            EventMessenger.Instance.AddListner<EventRemoveItemFromInventory>(OnRemoveItemEvent);
        }

        protected virtual void OnDisable()
        {
            EventMessenger.Instance.RemoveListner<EventAddItemToInventory>(OnAddItemEvent);
            EventMessenger.Instance.RemoveListner<EventRemoveItemFromInventory>(OnRemoveItemEvent);
        }

        private void OnRemoveItemEvent(EventRemoveItemFromInventory e)
        {
            if (e.InventoryUUID == InventoryUUID)
            {
                if (e.Item == null)
                {
                    Debug.LogWarning("trying to remove a null item");
                }
                else
                {
                    RemoveItem(e.Item, true);
                }
            }
        }

        protected void Start()
        {
            InventoryManager.Instance.AddInventory(this);
        }

        private void OnAddItemEvent(EventAddItemToInventory e)
        {
            if (e.InventoryUUID == InventoryUUID)
            {
                if (e.Item == null)
                {
                    Debug.LogWarning("Trying to add a null item.");
                }
                else
                {
                    AddItem(e.Item, e.UpdateUI);
                }
            }
        }

        public IItem GetItem(string uniqueUUID)
        {
            for (int i = 0; i < InternalItems.Count; i++)
            {
                IItem resultItem = InternalItems[i];
                if (resultItem.Data.ItemUUID == uniqueUUID)
                {
                    return resultItem;
                }
            }

            return null;
        }

        public void AddItem(IItem item, bool updateUI)
        {
            if (CheckIfExists(item.Data.ItemUUID) == false) // We dont have the item when false
            {
                if (CanAddItem)
                {
                    InternalItems.Add(item);
                    EventSystem.EventMessenger.Instance.Raise(new Events.EventAddItemToInventory(InventoryUUID, item, updateUI));
                    
                }
                else
                {
                    Debug.LogWarning("The inventory is full with [" + InventoryMaxItems + "] items.");
                }
            }
            else
            {
                Debug.LogError("The item with the unique uuid of [" + item.Data.ItemUUID + "] is already in the inventory.");
            }
        }

        public void RemoveItem(IItem item, bool updateUI)
        {
            RemoveItem(item.Data.ItemUUID, updateUI);
        }

        public void RemoveItem(string uniqueUUID, bool updateUI)
        {
            #region Linq Version
            //EventSystem.EventMessager.Instance.Raise(new Events.EventBeforeRemoveInventoryItem(item));
            //InternalItems.RemoveAll(item => item.Data.UniqueUUID == uniqueUUID);
            //EventSystem.EventMessager.Instance.Raise(new Events.EventAfterRemoveInventoryItem(item));
            #endregion Linq Version

            #region Foreach Version
            foreach (IItem item in InternalItems.ToArray())
            {
                if (item.Data.ItemUUID == uniqueUUID)
                {
                    InternalItems.Remove(item);
                    EventSystem.EventMessenger.Instance.Raise(new Events.EventRemoveItemFromInventory(InventoryUUID, item));
                }
            }
            #endregion Foreach Version
        }

        public void UpdateItem(string uniqueUUID, IItem item, bool updateUI)
        {
            for (int i = 0; i < InternalItems.Count; i++)
            {
                if (InternalItems[i].Data.ItemUUID == uniqueUUID)
                {
                    InternalItems[i] = item;
                    EventSystem.EventMessenger.Instance.Raise(new Events.EventUpdateInventoryItem(InventoryUUID, item));
                }
            }
        }

        public void RemoveAllItems(bool updateUI = true)
        {
            //Refactore
            InternalItems.Clear();
        }

        private bool CheckIfExists(string uniqueUUID)
        {
            for (int i = 0; i < InternalItems.Count; i++)
            {
                if (InternalItems[i].Data.ItemUUID == uniqueUUID)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanAddItem
        {
            get { return ItemsCount < InventoryMaxItems; }
        }
    }
}