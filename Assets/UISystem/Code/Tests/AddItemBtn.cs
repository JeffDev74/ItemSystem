using FPS.EventSystem;
using FPS.InventorySystem;
using FPS.InventorySystem.Events;
using FPS.ItemSystem;
using UnityEngine;


namespace FPS
{
    public class AddItemBtn : MonoBehaviour
    {
        private string ItemA_ID = "9e371875-d6be-43c2-a254-d74f0893df59";

        public string inventory_uuid = ""; //"00071875-d6be-43c2-a254-d74f0893d000";

        public Sprite itemIcon;

        public IInventory _inventory;
        public IInventory Inventory
        {
            get
            {
                if (_inventory == null)
                {
                    _inventory = InventoryManager.Instance.GetInventoryByUUID(inventory_uuid);
                }
                return _inventory;
            }
        }

        public void AddItem()
        {
            EventMessenger.Instance.Raise(new EventAddItemToInventory(Inventory.InventoryUUID, FactoryItem(), true));
        }

        public ICoreData FactoryItem()
        {
            // Resource item
            ICoreData testItem = new ResourceItem();
            testItem.BaseData.ID = 1;
            testItem.BaseData.ItemUUID = System.Guid.NewGuid().ToString();// ItemA_ID;
            testItem.BaseData.ItemName = "AK47";
            testItem.BaseData.Quantity = 777;
            testItem.BaseData.Description = "Weapon mid-range";
            testItem.BaseData.InventoryUUID = Inventory.InventoryUUID;
            testItem.Inventory = Inventory;

            testItem.BaseNSData = new ResourceNSData();

            testItem.BaseNSData.Icon = itemIcon;

            return testItem as ICoreData;
        }
    }
}