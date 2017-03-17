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
        public string inventory_uuid = "7b9e8423-8291-4a75-a935-0704dfee50f0";
        public string actionBayInventory_uuid = "29328eda-bd66-4961-8955-46162654a07e";

        public Sprite itemIcon;

        

        public void AddItem()
        {
            SetupInventory();
            EventMessenger.Instance.Raise(new EventAddItemToInventory(inventory_uuid, FactoryItem(), true));
        }

        public void AddItemToActionBar()
        {
            SetupInventory();
            EventMessenger.Instance.Raise(new EventAddItemToInventory(actionBayInventory_uuid, FactoryItem(), true));
        }

        private void SetupInventory()
        {
            IUIPanel mainInventoryPanel = UIManager.Instance.MainInventoryUIPanel;
            UIInventory uiMainInventory = mainInventoryPanel.TheTransform.GetComponent<UIInventory>();
            IInventory mainInventory = InventoryManager.Instance.GetInventoryByUUID(inventory_uuid);
            uiMainInventory.InventoryUUID = mainInventory.InventoryUUID;
            uiMainInventory.SetupSlots();

            IUIPanel actionBarPanel = UIManager.Instance.ActionBarUIPanel;
            UIInventory uiActionBarInventory = actionBarPanel.TheTransform.GetComponent<UIInventory>();
            IInventory actionBarInventory = InventoryManager.Instance.GetInventoryByUUID(actionBayInventory_uuid);
            uiActionBarInventory.InventoryUUID = actionBarInventory.InventoryUUID;
            uiActionBarInventory.SetupSlots();
        }

        public ICoreData FactoryItem()
        {
            return SODatabaseManager.Instance.ResourceSODBModel.GetById<BaseItem>(1);
        }
    }
}