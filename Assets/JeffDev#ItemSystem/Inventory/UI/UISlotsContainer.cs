using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPS.InventorySystem.UI
{
    public class UISlotsContainer : MonoBehaviour
    {
        private List<UISlot> _uiSlotList;
        public List<UISlot> UISlotList
        {
            get
            {
                if (_uiSlotList == null) { _uiSlotList = new List<UISlot>(); }
                if (_uiSlotList.Count <= 0)
                {
                    _uiSlotList = new List<UISlot>();
                    UISlot[] uiInventorySlots = transform.GetComponentsInChildren<UISlot>();
                    _uiSlotList = uiInventorySlots.ToList();
                }
                return _uiSlotList;
            }
        }

        private List<UIItem> _inventoryItems;
        public List<UIItem> InventoryItems
        {
            get
            {
                if (_inventoryItems == null)
                {
                    _inventoryItems = new List<UIItem>(UISlotList.Count);
                }

                _inventoryItems.Clear();

                for (int i = 0; i < UISlotList.Count; i++)
                {
                    if (UISlotList[i].ThisUIItem != null)
                    {
                        _inventoryItems.Add(UISlotList[i].ThisUIItem);
                    }
                }
                return _inventoryItems;
            }
        }

        public int SlotsCount
        {
            get { return UISlotList.Count; }
        }

        public void UpdateSlotsIds()
        {
            for (int i = 0; i < UISlotList.Count; i++)
            {
                if (UISlotList[i] != null)
                {
                    UISlotList[i].ID = i;
                    UISlotList[i].name = "UISlot_" + i;
                }
            }
        }

        public void Awake()
        {
            UpdateSlotsIds();
        }

        public void RemoveItemByUUID(string itemUUID)
        {
            Debug.Log("Will remove item [" + itemUUID + "]");

            foreach (UIItem invItem in InventoryItems)
            {
                if (invItem.IsActive)
                {
                    if (invItem.Item.BaseData.UniqueUUID == itemUUID)
                    {
                        invItem.DestroyItem();
                    }
                }
           }
        }

        public UISlot GetSlot()
        {
            for (int i = 0; i < UISlotList.Count; i++)
            {
                if (UISlotList[i].IsFree)
                {
                    return UISlotList[i];
                }
            }
            return null;
        }

        public UISlot GetSlotByID(int slotID)
        {
            return null;
        }
    }
}