﻿using FPS.EventSystem;
using FPS.ItemSystem;
using UnityEngine;

namespace FPS.InventorySystem.Events
{
	public class EventItemWasUpdatedOnInventory : GameEvent
	{
        private string _inventoryUUID;
        public string InventoryUUID
        {
            get { return _inventoryUUID; }
            set { _inventoryUUID = value; }
        }

        private ICoreData _item;
        public ICoreData Item
        {
            get { return _item; }
            private set { _item = value; }
        }

        private bool _updateUI;
        public bool UpdateUI
        {
            get { return _updateUI; }
            private set { _updateUI = value; }
        }

        public EventItemWasUpdatedOnInventory(string inventoryUUID, ICoreData item, bool updateUI = true)
        {
            this.InventoryUUID = inventoryUUID;
            this.Item = item;
            this.UpdateUI = updateUI;
        }
    }
}