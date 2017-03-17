﻿using FPS.EventSystem;
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


            EventMessenger.Instance.Raise(new EventAddItemToInventory(inventory_uuid, FactoryItem(), true));
        }

        public ICoreData FactoryItem()
        {
            // Resource item
            ICoreData testItem = new ResourceItem();
            testItem.BaseData.ID = 1;
            testItem.BaseData.UniqueUUID = System.Guid.NewGuid().ToString();
            testItem.BaseData.Name = "Wood";
            IStackableData iStackData = testItem as IStackableData;
            if(iStackData != null)
            {
                iStackData.Quantity = 777;
            }

            testItem.BaseData.Description = "Wood Resource";
          
            testItem.BaseNSData = new ResourceNSData();
            testItem.BaseNSData.Icon = itemIcon;
            return testItem as ICoreData;
        }

        public interface WeaponAllData : ISData, IStackableData
        {

        }
    }
}