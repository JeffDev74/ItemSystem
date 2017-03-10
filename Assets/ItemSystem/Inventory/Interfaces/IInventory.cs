using System.Collections.Generic;
using UnityEngine;
namespace FPS.InventorySystem
{
    public interface IInventory
    {
        Transform TheTransform { get; }
        string InventoryUUID { get; set; }
        List<IItem> Items { get; }
        int ItemsCount { get; }
        IItem GetItem(string uniqueUUID);
        void AddItem(IItem item, bool updateUI);
        void RemoveItem(IItem item, bool updateUI);
        void RemoveItem(string uniqueUUID, bool updateUI);
        void UpdateItem(string uniqueUUID, IItem item, bool updateUI);
        void RemoveAllItems(bool updateUI = true);

        bool CanAddItem { get; }
    }
}