﻿using FPS.EventSystem;
using FPS.InventorySystem;
using FPS.InventorySystem.Events;
using FPS.ItemSystem;
using UnityEngine;

namespace FPS
{
    public class UITests : MonoBehaviour
    {
        private string ItemA_ID = "9e371875-d6be-43c2-a254-d74f0893df59";

        private IInventory _inventory;
        public IInventory Inventory
        {
            get
            {
                if (_inventory == null)
                {
                    _inventory = GetComponent<IInventory>();
                }
                return _inventory;
            }
        }

        private void Start()
        {

            ResourceItem weapon = new ResourceItem();
            weapon.BaseData.ID = 1;
            weapon.BaseData.ItemUUID = ItemA_ID;
            weapon.BaseData.ItemName = "AK47";
            weapon.BaseData.Description = "Weapon mid-range";

            ICoreData item = weapon as ICoreData;

            // Debug.Log("I have [" + Inventory.ItemsCount + "] items.");
            //// The event was changed and we should no be passing the inventory interface anymore
            //// only the inventory uuid
            //EventMessenger.Instance.Raise(new EventAddItemToInventory(Inventory, Inventory.InventoryUUID, item));
            //Debug.Log("I have [" + Inventory.ItemsCount + "] items.");
        }
    }
}