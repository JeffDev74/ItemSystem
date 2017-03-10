using UnityEngine;
using UnityEngine.EventSystems;

namespace FPS.InventorySystem.UI
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private UIItem _uiItem;
        public UIItem ThisUIItem
        {
            get
            {
                _uiItem = GetComponentInChildren<UIItem>();
                return _uiItem;
            }
        }

        public bool IsFree
        {
            get
            {
                return ThisUIItem.Item == null;
            }
        }

        public void UpdateSlotItem(IItem item, bool updateUI = true)
        {
            ThisUIItem.Item = item;
            UpdateItemData();

            if (updateUI)
            {
                ThisUIItem.UpdateUI();
            }
        }

        private void UpdateItemData()
        {
            if (ThisUIItem.Item != null)
            {
                // Any variable on the item that 
                // must be set should be set here.
                ThisUIItem.Item.Data.SlotId = ID;
                ThisUIItem.Item.NSData.Slot = this;
            }
            else
            {
                Debug.LogWarning("We are trying to update a null slot item.");
            }
        }

        private Transform _theTransform;
        private Transform TheTransform
        {
            get
            {
                if (_theTransform == null)
                {
                    _theTransform = transform;
                }
                return transform;
            }
        }

        private string _inventoryUUID;
        public string InventoryUUID
        {
            get { return _inventoryUUID; }
            set { _inventoryUUID = value; }
        }

        private IInventory _inventory;
        private IInventory Inventory
        {
            get
            {
                if (_inventory == null)
                {
                    _inventory = InventoryManager.Instance.GetInventoryByUUID(InventoryUUID);
                }
                return _inventory;
            }
        }

        private void AttachUIItem(UIItem uiItem)
        {
            uiItem.transform.SetParent(TheTransform);

            if (uiItem.Item != null)
            {
                uiItem.Item.Data.SlotId = ID;
                uiItem.Item.NSData.Slot = this;
            }
        }

        private bool IsCrossInventory
        {
            get
            {
                return InventoryUUID != UIItem.DraggedItemStartSlot.InventoryUUID;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (IsFree)
            {
                // does this slot allow this item
                // should take one return the rest?

                if (IsCrossInventory) // same inevntory
                {
                    Debug.Log("Drag cross inv");
                    // add the item to the inventory where it is being dragged to
                    EventSystem.EventMessenger.Instance.Raise(new Events.EventRemoveItemFromInventory(UIItem.DraggedItemStartSlot.InventoryUUID, UIItem.DraggedItem.Item, false));

                    // remove the item from the inventory the item is being dragged from
                    //EventSystem.EventMessenger.Instance.Raise(new Events.EventAddItemToInventory(InventoryUUID, UIItem.DraggedItem.Item, false));

                    SwapUIItems();

                    // Remove the item from this inventory
                    // Add this item to the other inventory
                }
                else
                {
                    // cross inventory drag
                    Debug.Log("drag same inventory");

                    SwapUIItems();

                    // if we are draggin items in the same inventory we
                    // just need to update the item information "Data"
                    // slot id ... etc.
                }
            }
            else
            {

                if (IsCrossInventory)
                {

                }
                else
                {

                }

                SwapUIItems();
                // The slot is occuped
                // is the item able to stack??
                // does the slot allow this item?
            }
        }

        private void SwapUIItems()
        {

            //UIItem._tmpItemBeingDragged.Item = UIItem.Item;
            AttachUIItem(UIItem.DraggedItem);
            //UpdateSlotItem(UIItem._tmpItemBeingDragged.Item);


            //UIItem.Item = UIItem._tmpItemBeingDragged.Item;
            UIItem.DraggedItemStartSlot.AttachUIItem(ThisUIItem);
            //UIItem._tmpItemStartSlot.UpdateSlotItem(UIItem.Item);

        }
    }
}