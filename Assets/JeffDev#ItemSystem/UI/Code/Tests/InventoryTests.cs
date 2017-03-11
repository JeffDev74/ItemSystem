using UnityEngine;
using FPS.InventorySystem;
using System.Collections.Generic;
using FPS.EventSystem;
using FPS.ItemSystem;

namespace FPS
{
    public class InventoryTests : MonoBehaviour
    {
        private string ItemA_ID = "9e371875-d6be-43c2-a254-d74f0893df59";
        private string ItemB_ID = "9e371875-d6be-43c2-a254-d74f0893df60";
        [SerializeField]
        private IInventory _theInventory;
        public IInventory TheInventory
        {
            get
            {
                if (_theInventory == null)
                {
                    _theInventory = GetComponent<IInventory>();
                }
                return _theInventory;
            }
            set
            {
                _theInventory = value;
            }
        }

        public bool shouldTestInventory = false;

        private void Start()
        {
            #region INVENTORY TESTS
            if (shouldTestInventory)
            {
                Debug.Log("Start Testing inventory UUID is [" + TheInventory.InventoryUUID + "]");

                // Testing adding item to the inventory
                ResourceItem ritem = new ResourceItem();
                ritem.BaseData.ID = 1;
                ritem.BaseData.UniqueUUID = ItemA_ID;
                ritem.BaseData.Name = "AK47";
                //ritem.BaseData.Description = "Weapon mid-range";

                Debug.Log(ritem.BaseData.Name);

                TestAddItemToInventory(ritem);

                // Get specific item
                TestGetSpecificItem(ritem);

                // Testing updating item on inventory
                ResourceItem ritem2 = new ResourceItem();
                ritem2.BaseData.ID = 1;
                ritem2.BaseData.UniqueUUID = ItemB_ID;
                ritem2.BaseData.Name = "AK47";
                ritem2.BaseData.Description = "Weapon mid-range altered.";

                TestUpdateItemInTheInventory(ritem2);

                // Test remove item
                TestRemoveItemFromInventory(ritem2);
                // Count all items
                TestCountAllItemsFromInventory(ritem, ritem2);

                // Get all items
                TestGetAllItemsFromInventory(ritem, ritem2);

            }
            #endregion INVENTORY TESTS

            //EventMessager.Instance.Raise(new EventTest(ItemA_ID));
        }

        private void OnEnable()
        {
            EventMessenger.Instance.AddListner<EventTest>(OnTestEvent);
        }

        private void OnDisable()
        {
            EventMessenger.Instance.RemoveListner<EventTest>(OnTestEvent);
        }

        private void OnTestEvent(EventTest e)
        {
            Debug.Log("PASSED [OnTestEvent] Received the event [" + e.GetType().Name + "] with item unique UUID [" + e.ItemUniqueUUID + "]");
        }

        #region INVENTORY TESTS
        private void TestAddItemToInventory(ICoreData testItem)
        {
            // Test add item
            TheInventory.AddItem(testItem, false);

            // Check item name and unique uuid
            ICoreData tmpBaseItem = TheInventory.GetItem(testItem.BaseData.UniqueUUID);
            if (tmpBaseItem == null)
            {
                Debug.LogError("FAILED [TestAddItemToInventory]. We should have the item in the inventory.");
            }
            else
            {
                Debug.Log("PASSED [TestAddItemToInventory]. Weapon description [" + tmpBaseItem.BaseData.Description + "] uniqueUUID [" + tmpBaseItem.BaseData.UniqueUUID + "]");
            }
        }

        private void TestGetSpecificItem(ICoreData testItem)
        {
            ICoreData tmpBaseItem = TheInventory.GetItem(testItem.BaseData.UniqueUUID);
            if (tmpBaseItem == null)
            {
                Debug.LogError("FAILED [TestGetSpecificItem]. We should have the item in the inventory.");
            }
            else
            {
                Debug.Log("PASSED [TestGetSpecificItem]. Weapon description [" + tmpBaseItem.BaseData.Description + "] uniqueUUID [" + tmpBaseItem.BaseData.UniqueUUID + "]");
            }
        }

        private void TestUpdateItemInTheInventory(ICoreData testItem)
        {
            TheInventory.UpdateItem(ItemA_ID, testItem, false);

            ICoreData tmpBaseItem = TheInventory.GetItem(ItemB_ID);
            if (tmpBaseItem == null)
            {
                Debug.LogError("FAILED [TestUpdateItemInTheInventory]. The item is missing from inventory.");
            }
            else
            {
                Debug.Log("PASSED [TestUpdateItemInTheInventory]. Weapon description [" + tmpBaseItem.BaseData.Description + "] uniqueUUID [" + tmpBaseItem.BaseData.UniqueUUID + "]");
            }
        }

        private void TestRemoveItemFromInventory(ICoreData testItem)
        {
            TheInventory.RemoveItem(testItem, false);
            ICoreData tmpBaseItem = TheInventory.GetItem(testItem.BaseData.UniqueUUID);
            if (tmpBaseItem == null)
            {
                Debug.Log("PASSED [TestRemoveItemFromInventory]. Weapon description [" + testItem.BaseData.Description + "] uniqueUUID [" + testItem.BaseData.UniqueUUID + "]");
            }
            else
            {
                Debug.LogError("FAILED [TestRemoveItemFromInventory]. We should have the item removed from the inventory.");
            }
        }

        private void TestCountAllItemsFromInventory(ICoreData itemA, ICoreData itemB)
        {
            TestAddItemToInventory(itemA);
            TestAddItemToInventory(itemB);

            if (TheInventory.ItemsCount == 2)
            {
                Debug.Log("PASSED [TestCountAllItemsFromInventory]. All items were acounted for.");
            }
            else
            {
                Debug.LogError("FAILED [TestCountAllItemsFromInventory]. We should have two items in the inventory.");
            }
            TheInventory.RemoveAllItems();
        }

        private void TestGetAllItemsFromInventory(ICoreData itemA, ICoreData itemB)
        {
            TestAddItemToInventory(itemA);
            TestAddItemToInventory(itemB);
            List<ICoreData> items = TheInventory.Items;
            if (items.Count == 2)
            {
                Debug.Log("PASSED [TestGetAllItemsFromInventory]. We got all the items.");
            }
            else
            {
                Debug.LogError("FAILED [TestGetAllItemsFromInventory]. We should have two items in the inventory.");
            }
            TheInventory.RemoveAllItems();
        }
        #endregion INVENTORY TESTS
    }
}