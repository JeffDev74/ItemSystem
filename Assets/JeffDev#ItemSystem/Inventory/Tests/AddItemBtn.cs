using FPS.EventSystem;
using FPS.InventorySystem;
using FPS.InventorySystem.Events;
using FPS.InventorySystem.UI;
using FPS.ItemSystem;
using FPS.UI;
using UnityEngine;


namespace FPS
{
    public class AddItemBtn : MonoBehaviour
    {
        public string inventory_uuid = "7df22de7-d039-4bcc-8805-efebc2f596fa";// "7ffec2c5-dfbd-4e78-b2db-33e95def41bb";

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
            IUIPanel panel = UIManager.Instance.MainInventoryUIPanel;
            UIInventory uiInventory = panel.TheTransform.GetComponent<UIInventory>();
            uiInventory.InventoryUUID = Inventory.InventoryUUID;
            EventMessenger.Instance.Raise(new EventAddItemToInventory(Inventory.InventoryUUID, FactoryItem(), true));
        }

        public ICoreData FactoryItem()
        {
            // Resource item
            ICoreData testItem = new ResourceItem();
            testItem.BaseData.ID = 1;
            testItem.BaseData.UniqueUUID = System.Guid.NewGuid().ToString();
            testItem.BaseData.Name = "Wood";
            testItem.BaseData.Quantity = 777;
            testItem.BaseData.Description = "Wood Resource";
            testItem.BaseData.InventoryUUID = Inventory.InventoryUUID;
            testItem.Inventory = Inventory;
            testItem.BaseNSData = new ResourceNSData();
            testItem.BaseNSData.Icon = itemIcon;
            return testItem as ICoreData;
        }
    }
}