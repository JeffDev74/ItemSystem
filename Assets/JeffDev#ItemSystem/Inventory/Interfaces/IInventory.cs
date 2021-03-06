﻿using FPS.ItemSystem;
using ItemSystem;
using System.Collections.Generic;
using UnityEngine;
namespace FPS.InventorySystem
{
    public interface IInventory
    {
        Transform TheTransform { get; }
        GUID UniqueGuid { get; }
        string InventoryUUID { get; }
        List<ICoreData> Items { get; }
        int ItemsCount { get; }
        ICoreData GetItem(string uniqueUUID);
        void AddItem(ICoreData item, bool updateUI);
        void RemoveItem(ICoreData item, bool updateUI);
        void RemoveItem(string uniqueUUID, bool updateUI);
        void UpdateItem(string uniqueUUID, ICoreData item, bool updateUI);
        void RemoveAllItems(bool updateUI = true);
        bool CanAddItem { get; }
    }
}