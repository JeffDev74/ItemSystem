using FPS.ItemSystem;
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
            set { _uiItem = value; }
        }

        public bool IsFree
        {
            get
            {
                return ThisUIItem.Item == null;
            }
        }

        public void UpdateSlotItem(ICoreData item, bool updateUI = true)
        {
            ThisUIItem.Item = item;
            ThisUIItem.IsActive = true;
            ThisUIItem.transform.SetParent(transform);

            UpdateSlotItemData();

            if (updateUI)
            {
                ThisUIItem.UpdateUI();
            }
        }

        private void UpdateSlotItemData()
        {
            if (ThisUIItem.Item != null)
            {
                ThisUIItem.Item.BaseData.InventoryUUID = InventoryUUID;
                ThisUIItem.Item.BaseData.SlotID = ID;
                ThisUIItem.Item.BaseNSData.Slot = this;
            }
            else
            {
                Debug.LogError("We are trying to update a null slot item.");
            }
        }

        private void AddUIItem(UIItem uiItem)
        {
            uiItem.transform.SetParent(transform);
            uiItem.Item.BaseData.SlotID = ID;
            uiItem.Item.BaseNSData.Slot = this;
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

        public bool HaveItem
        {
            get
            {
                if (ThisUIItem != null)// & ThisUIItem.Item)
                {
                    return ThisUIItem.IsActive;
                }
                return false;
            }
        }

        private bool IsCrossInventory
        {
            get { return InventoryUUID != UIItem.DraggedItemStartSlot.InventoryUUID; }
        }

        #region IDropHandler implementation
        public void OnDrop(PointerEventData eventData)
        {
            if (UIItem.DraggedItem == null) return;
            if (UIItem.DraggedItem.Item == null) return;

            if (HaveItem == false)
            {
                AddUIItem(UIItem.DraggedItem);
                
                //if (UIItem.DraggedItem.Item.BaseData.InventoryUUID != InventoryUUID)
                if(IsCrossInventory)
                {
                    Debug.Log("EMPTY CROSS PANEL");

                    // Remove item from inventory that its coming from
                    //UIInventoryItem.tmpItemBeingDragged.Item.Inventory.RemoveItemByUUID(UIInventoryItem.tmpItemBeingDragged.Item.BaseData.UniqueUUID, false);
                    //EventMessenger.Instance.Raise(new EventRemoveItemFromInventory(UIInventoryItem.tmpItemBeingDragged.Item.Inventory.UniqueUUID, UIInventoryItem.tmpItemBeingDragged.Item, false));
                    // --> EventMessenger.Instance.Raise(new EventRemoveItemFromInventory(UIInventoryItem.tmpItemBeingDragged.Item.BaseData.InventoryUniqueUUID, UIInventoryItem.tmpItemBeingDragged.Item, false));

                    // is this right? i am setting the panel id here and removing item bellow? will this afect anything?
                    UIItem.DraggedItem.Item.BaseData.InventoryUUID = InventoryUUID;

                    // IMPORTANT SET THE INVENTORY
                    // IMPORTANT SET THE INVENTORY
                    //this.ThisUIItem.Item.Inventory = ThisUIItem.Item.Inventory;//  InventoryPanel.Inventory;

                    // Add Item to the new inventory
                    //InventoryPanel.Inventory.AddItemNoStack(UIInventoryItem.tmpItemBeingDragged.Item, false);
                    // --> EventMessenger.Instance.Raise(new EventAddItemToInventory(InventoryPanel.Inventory.UniqueUUID, UIInventoryItem.tmpItemBeingDragged.Item, false, false));

                    this.ThisUIItem.transform.SetParent(UIItem.DraggedItemStartSlot.transform);

                    // ADDED FOR ACTION BAR
                    // --> EventMessenger.Instance.Raise(new EventUIInventoryItemAdded(InventoryPanel, UIInventoryItem.tmpItemBeingDragged));
                }
                else
                {
                    Debug.Log("EMPTY SAME PANEL");
                    // IMPORTANT SET THE INVENTORY
                    // IMPORTANT SET THE INVENTORY
                    //UIItem.DraggedItem.Item.Inventory = ThisUIItem.Item.Inventory;// InventoryPanel.Inventory;
                    this.ThisUIItem.transform.SetParent(UIItem.DraggedItemStartSlot.transform);
                }
            }
            else
            {
                // If the item in the slot is stackable check here too and dont return to original position
                // if we have an item in the slot where this slot is being dragged return to original position


                //Debug.Log("The item is being dropped on top or another item");

                // Set the start slot ItemContainer to this slot item container
                UIItem.DraggedItemStartSlot.ThisUIItem = this.ThisUIItem;

                // Here I can change where to check for the stackable interface
                // could be in the item itself (ItemContainer.Item)
                // or it could be in the data (ItemContainer.Item.BaseData)
                IStackable stackInterface = ThisUIItem.Item as IStackable;
                if (stackInterface != null && stackInterface.IsStackable)
                {
                    StackResult stackResult = stackInterface.Stack(UIItem.DraggedItem.Item as BaseItem);
                    if (stackResult != null)
                    {
                        if ((stackResult.item.BaseData as IStackableData).Quantity <= 0)
                        {
                            //Debug.Log("ITEMS WERE STACKED RESULT QUANTITY IS <= 0");

                            // Set the slot ItemContainer that the item is being dragged from to null
                            UIItem.DraggedItemStartSlot.ThisUIItem = ThisUIItem;

                            //InventoryItem.Item.BaseData.Quantity = stackResult.leftItem.BaseData.Quantity;
                            ThisUIItem.UpdateQuantity();

                            //if (UIItem.DraggedItem.Item.BaseData.InventoryUUID != InventoryPanel.Inventory.UniqueUUID)
                            if(IsCrossInventory)
                            {
                                Debug.Log("STACK WITH DESTROY CROSS PANEL");
                                // --> EventMessenger.Instance.Raise(new EventUIInventoryItemChanged(InventoryPanel.Inventory.UniqueUUID, InventoryItem.Item.BaseData));
                                ////ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged(InventoryItem, null));
                                ////ExecuteEvents.ExecuteHierarchy<IHasChanged>(UIInventoryItem.tmpItemStartSlot.gameObject, null, (x, y) => x.HasChanged(InventoryItem, null));
                            }
                            else
                            {
                                Debug.Log("STACK WITH DESTROY SAME PANEL");

                                UIItem.DraggedItem.transform.SetParent(UIItem.DraggedItemStartSlot.transform);
                                // --> EventMessenger.Instance.Raise(new EventUIInventoryItemChanged(InventoryPanel.Inventory.UniqueUUID, InventoryItem.Item.BaseData));

                                ////ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged(InventoryItem, null));
                            }

                        }
                        else
                        {
                            //Debug.Log("ITEMS WERE STACKED BOTH ITEMS STILL HAVE QUANTITY");
                            // right item (dragged item) exchanged the quantities but 
                            // it still have quantity
                            // put it back where it came from
                            UIItem.DraggedItem.transform.SetParent(UIItem.DraggedItemStartSlot.transform);

                            // update the left (item in this slot) item quantity
                            ThisUIItem.UpdateQuantity();
                            // update right item (dragged item) quantity
                            UIItem.DraggedItem.UpdateQuantity();


                            //EventMessenger.Instance.Raise(new EventUIInventoryItemChanged(InventoryItem.Slot.InventoryPanel.Inventory.UniqueUUID, InventoryItem.Item.BaseData));
                        }

                        // Since the item being dragged may be destroyed se send an event
                        // on dropped on slot with the item already in the slot
                        // EventMessenger.Instance.Raise(new EventUIInventoryItemDropedOnSlot(UIInventoryItem.tmpItemBeingDragged));
                    }
                    else
                    {
                        // stack failed for some reason maybe the items were not stackable
                        // or the left item (item in the slot) was full. Do a switch;
                        Debug.Log("STACK IS NULL DO A SWITCH ON THE ITEMS");
                        SwapNonStackable();
                    }
                }
                else
                {
                    Debug.LogError("Item [" + ThisUIItem.Item.BaseData.Name + "] Does not implement the IStackable interface. Or the item is not marked as stackable " + "[" + ThisUIItem.GetType() + "]");
                    //Debug.Log("ITEM COREDATA IS MARKED AS NON STACKABLE SWAP ITEMS");
                    SwapNonStackable();
                }

                ThisUIItem.UpdateSlotInfo();

                // When stacking an item the inventory item of tmpItemStartSlot will
                // be null. Lets check that and only update the slot info if its not null
                if (UIItem.DraggedItemStartSlot.ThisUIItem != null)
                {
                    UIItem.DraggedItemStartSlot.ThisUIItem.UpdateSlotInfo();
                }
            }

            //// There is a change reset the item container and item
            //InventoryItem = null;
            //UIInventoryItemTransform = null;

            // Event used by the crafting system to recalculate items if inventory changes
            // --> EventMessenger.Instance.Raise(new EventUIInventoryChanged(InventoryPanel.Inventory));
            //EventMessenger.Instance.Raise(new EventUIInventoryItemChanged(InventoryPanel.Inventory.UniqueUUID, InventoryItem.Item.BaseData));

            //ResetDropVariables();
        }
        #endregion

        public void SwapNonStackable()
        {
            //int itemBeingDraggedSlotid = UIInventoryItem.tmpItemStartSlot.id;
            //string itemBeingDraggedSlotName = UIInventoryItem.tmpItemStartSlot.name;

            //int itemSlotid = id;
            //string itemSlotName = transform.name;
            //Debug.Log("Xresearch dragged item slot name [" + itemBeingDraggedSlotName + "] item slotname [" + itemSlotName + "]");

            //// Update item base data slot info before set parent
            //InventoryItem.Item.BaseData.InventorySlotId = itemBeingDraggedSlotid;
            //InventoryItem.Item.BaseData.InventorySlotName = itemBeingDraggedSlotName;
            //InventoryItem.Item.BaseNSData.InventoryUISlot = UIInventoryItem.tmpItemStartSlot;


            //UIInventoryItem.tmpItemBeingDragged.Item.BaseData.InventorySlotId = itemSlotid;
            //UIInventoryItem.tmpItemBeingDragged.Item.BaseData.InventorySlotName = itemSlotName;
            //UIInventoryItem.tmpItemBeingDragged.Item.BaseNSData.InventoryUISlot = this;

            // Set the dragged item to this slot
            UIItem.DraggedItem.transform.SetParent(transform);

            if(IsCrossInventory)
            //if (UIItem.DraggedItem.Item.BaseData.InventoryUUID != InventoryUUID)
            {
                //Debug.Log("SWAP NON STACKABLE ITEM CROSS PANEL");

                // Remove dragged item from its inventory
                //UIInventoryItem.tmpItemBeingDragged.Item.Inventory.RemoveItemByUUID(UIInventoryItem.tmpItemBeingDragged.Item.BaseData.UniqueUUID, false);
                // --> EventMessenger.Instance.Raise(new EventRemoveItemFromInventory(UIInventoryItem.tmpItemBeingDragged.Item.Inventory.UniqueUUID, UIInventoryItem.tmpItemBeingDragged.Item, false));

                UIItem.DraggedItem.Item.BaseData.InventoryUUID = InventoryUUID;

                ThisUIItem.Item.Inventory = UIItem.DraggedItem.Item.Inventory;

                // Add this item to the dragged item inventory
                //UIInventoryItem.tmpItemBeingDragged.Item.Inventory.AddItem(InventoryItem.Item, false);
                // --> EventMessenger.Instance.Raise(new EventAddItemToInventory(UIInventoryItem.tmpItemBeingDragged.Item.Inventory.UniqueUUID, InventoryItem.Item, false, false));

                // ADDED FOR ACTION BAR
                // --> EventMessenger.Instance.Raise(new EventUIInventoryItemAdded(UIInventoryItem.tmpItemStartSlot.InventoryPanel, InventoryItem));


                // Remove this item from its inventory
                //InventoryPanel.Inventory.RemoveItemByUUID(InventoryItem.Item.BaseData.UniqueUUID, false);
                // --> EventMessenger.Instance.Raise(new EventRemoveItemFromInventory(InventoryPanel.Inventory.UniqueUUID, InventoryItem.Item, false));

                ThisUIItem.Item.BaseData.InventoryUUID = UIItem.DraggedItemStartSlot.InventoryUUID;

                // IMPORTANT SET INVENTORY
                // --> UIItem.DraggedItemd.Item.Inventory = InventoryPanel.Inventory;

                // Add the dragged item to this inventory
                //InventoryPanel.Inventory.AddItem(UIInventoryItem.tmpItemBeingDragged.Item, false);
                // --> EventMessenger.Instance.Raise(new EventAddItemToInventory(InventoryPanel.Inventory.UniqueUUID, UIInventoryItem.tmpItemBeingDragged.Item, false, false));

                // ADDED FOR ACTION BAR
                // --> EventMessenger.Instance.Raise(new EventUIInventoryItemAdded(InventoryPanel, UIInventoryItem.tmpItemBeingDragged));

                // Set this item to the dragged item slot
                ThisUIItem.transform.SetParent(UIItem.DraggedItemStartSlot.transform);

                ////ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged(InventoryItem, null));
                ////ExecuteEvents.ExecuteHierarchy<IHasChanged>(UIInventoryItem.tmpItemStartSlot.gameObject, null, (x, y) => x.HasChanged(InventoryItem, null));
            }
            else
            {
                //Debug.Log("SWAP NON STACKABLE ITEM SAME PANEL");

                // Set this item to the dragged item slot
                ThisUIItem.transform.SetParent(UIItem.DraggedItemStartSlot.transform);

                ////ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged(InventoryItem, null));
            }

            //EventMessenger.Instance.Raise(new EventUIInventoryItemChanged(InventoryPanel.Inventory.UniqueUUID, InventoryItem.Item.BaseData));
            //EventMessenger.Instance.Raise(new EventUIInventoryItemChanged(UIInventoryItem.tmpItemBeingDragged.Item.Inventory.UniqueUUID, UIInventoryItem.tmpItemBeingDragged.Item.BaseData));
        }

    }
}