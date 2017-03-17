using FPS.ItemSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FPS.InventorySystem.UI
{
    public class UIItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static UIItem DraggedItem;
        public static UISlot DraggedItemStartSlot;
        private Vector3 _tmpItemStartPosition;

        public static string InventoryUUID { get; set; }

        private CanvasGroup _theCanvasGroup;
        private CanvasGroup TheCanvasGroup
        {
            get
            {
                if (_theCanvasGroup == null)
                {
                    _theCanvasGroup = GetComponent<CanvasGroup>();
                }
                return _theCanvasGroup;
            }
        }

        private Canvas _mainCanvas;
        private Canvas MainCanvas
        {
            get
            {
                if (_mainCanvas == null)
                {
                    // Refactore we may have more then one canvas
                    _mainCanvas = FindObjectOfType<Canvas>();
                }
                return _mainCanvas;
            }
        }

        protected ICoreData _item;
        public ICoreData Item
        {
            get { return _item; }
            set
            {
                _item = value;
                UpdateUI();
            }
        }

        public UISlot Slot
        {
            get { return GetComponentInParent<UISlot>(); }
        }

        private UISlot _theUISlot;
        public UISlot GetSlot(bool force = false)
        {
            if (_theUISlot == null)
            {
                _theUISlot = GetComponentInParent<UISlot>();
            }

            if (force)
            {
                _theUISlot = GetComponentInParent<UISlot>();
            }

            return _theUISlot;
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

        public Sprite DefaultImage;

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        [SerializeField]
        private GameObject _iconGO;
        private GameObject IconGO
        {
            get
            {
                if (_iconGO == null)
                {
                    _iconGO = TheTransform.FindChild("Icon").gameObject;
                }
                return _iconGO;
            }
        }

        private Image _icon;
        private Image Icon
        {
            get
            {
                if (_icon == null)
                {
                    _icon = IconGO.GetComponent<Image>();
                }
                return _icon;
            }
            set { _icon = value; }
        }

        [SerializeField]
        private GameObject _quantityTextGO;
        private GameObject QuantityTextGO
        {
            get
            {
                if (_quantityTextGO == null)
                {
                    _quantityTextGO = TheTransform.FindChild("QuantityText").gameObject;
                }
                return _quantityTextGO;
            }
        }

        private Text _quantityText;
        private Text QuantityText
        {
            get
            {
                if (_quantityText == null)
                {
                    _quantityText = QuantityTextGO.GetComponent<Text>();
                }
                return _quantityText;
            }
        }

        [SerializeField]
        private GameObject _wornBarGO;
        private GameObject WornBarGO
        {
            get
            {
                if (_wornBarGO == null)
                {
                    _wornBarGO = TheTransform.FindChild("DamageBar").gameObject;
                }
                return _wornBarGO;
            }
        }

        private Image _wornBarImage;
        private Image WornBarImage
        {
            get
            {
                if (_wornBarImage == null)
                {
                    _wornBarImage = WornBarGO.GetComponent<Image>();
                }
                return _wornBarImage;
            }
        }

        private void Start()
        {
            ResetUI();
        }

        public void ResetUI()
        {
            Icon.sprite = DefaultImage;
            QuantityText.text = "0";
            QuantityTextGO.SetActive(false);
            WornBarGO.SetActive(false);
            TheCanvasGroup.interactable = false;
            TheCanvasGroup.blocksRaycasts = false;
        }

        public void DestroyItem()
        {
            IsActive = false;
            Item = null;

            Icon.sprite = DefaultImage;
            QuantityText.text = "";
            WornBarImage.fillAmount = 0;
        }

        public void UpdateUI()
        {
            TheCanvasGroup.interactable = Item == null ? false : true;
            TheCanvasGroup.blocksRaycasts = Item == null ? false : true;

            UpdateIcon();
            UpdateQuantity();
            UpdateDamageBar();
        }

        public void UpdateIcon(bool isdefault = false)
        {
            if (Item == null)
            {
                Icon.sprite = DefaultImage;
            }
            else
            {
                Icon.sprite = Item.BaseNSData.Icon;
            }
        }

        public void UpdateQuantity()
        {
            if (Item == null)
            {
                QuantityText.text = "0";
                QuantityTextGO.SetActive(false);
            }
            else
            {
                QuantityText.text = Item.BaseData.Quantity.ToString();
                QuantityTextGO.SetActive(true);
            }
        }

        public void UpdateDamageBar()
        {
            if (Item == null)
            {
                WornBarGO.SetActive(false);
            }
            else
            {
                WornBarGO.SetActive(true);
            }
        }

        public void ToggleQuantity(bool state)
        {
            // Refactore to use canvas group instead of disabling the item
            // If we disable the item and the item have scripts that listen
            // for events, those events will be missed if the GO is inactive
            // To avoid that we could use a canvas group and just  "fade-out" the GO
            if (QuantityTextGO)
            {
                QuantityTextGO.gameObject.SetActive(state);
            }
        }

        public void ToggleDamageBar(bool state)
        {
            if (WornBarGO)
            {
                WornBarGO.gameObject.SetActive(state);
            }
        }

        public void UpdateSlotInfo()
        {
            //Debug.Log("Updating slot info for item [" + Item.BaseData.ItemName + "] [" + Item.BaseData.Quantity + "]");
            Item.BaseData.SlotID = Slot.ID;
            Item.BaseNSData.Slot = Slot;
            Item.BaseData.InventoryUUID = Slot.InventoryUUID;
        }

        #region IBeginDragHandler implementation
        public void OnBeginDrag(PointerEventData eventData)
        {
            DraggedItem = this;
            _tmpItemStartPosition = transform.position;

            //// Set the tmpStart slot to our slot;
            DraggedItemStartSlot = Slot;

            //// ATTATCH THE SlotItemContainer to the MAIN-UI (UIManager)
            //Canvas c = Slot.InventoryPanel.GetComponentInParent<UIManager>();

            //// Make the item chield of the main canvas to avoid 
            //// the icon override (slot under another) when dragging the item
            //if (uiManager != null)
            //{
            transform.SetParent(MainCanvas.transform);// uiManager.transform);
            //}

            TheCanvasGroup.blocksRaycasts = false;
        }
        #endregion

        #region IDragHandler implementation
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;

        }
        #endregion

        #region IEndDragHandler implementation
        public void OnEndDrag(PointerEventData eventData)
        {
            //if (Item == null) return;

            // If the parent slot is the same move the item back to its
            // start position
            if (transform.parent == DraggedItemStartSlot)
            {
                transform.position = _tmpItemStartPosition;
                Debug.Log("slot changed setting position");
            }

            TheCanvasGroup.blocksRaycasts = true;

            // If The item is dragged outside a valid slot remove it
            //UIInventorySlot _tmpSlot = transform.parent.GetComponent<UIInventorySlot>();
            if (Slot == null)
            {
                //Debug.Log("DRAGGED OUTSIDE INVENTORY");

                // Put the item back to its place
                transform.SetParent(DraggedItemStartSlot.transform);

                //tmpItemStartSlot.InventoryPanel.Inventory.RemoveItem(Item, false);
                // --> DraggedItemStartSlot.InventoryPanel.Inventory.RemoveItemByUUID(Item.BaseData.UniqueUUID, false);

                // If the item was stacked the slot will be null as well
                // so we check if the quantity is greater then 0 to generate drop
                // Trigger event to generate inventory drop
                if (Item.BaseData.Quantity > 0)
                {
                    // --> EventMessenger.Instance.Raise(new EventInventoryGenerateDrop(DraggedItemStartSlot.InventoryPanel.Inventory, Item));
                }

                // The action bar listen for this event
                // If the current active item in the action bar
                // is this item it will be disabled
                // Also the UIMain panel listen for this event
                // to trigger the event to remove the item from the inventory
                //EventMessenger.Instance.Raise(new EventUIInventoryItemRemove(tmpItemStartSlot.InventoryPanel, this));

                // Event used by crafting system to recalculate items if inventory changes.
                // --> EventMessenger.Instance.Raise(new EventUIInventoryChanged(DraggedItemStartSlot.InventoryPanel.Inventory));


                DestroyItem();

                return;
            }

            if (Item.BaseData.Quantity <= 0)
            {
                //Debug.Log("ITEM HAVE NO QUANTITY REMOVING");
                // --> DraggedItemStartSlot.InventoryPanel.Inventory.RemoveItemByUUID(Item.BaseData.UniqueUUID, false);
                DestroyItem();
                return;
            }

            UpdateSlotInfo();

            //EventMessenger.Instance.Raise(new EventUIInventoryItemChanged(Slot.InventoryPanel.Inventory.UniqueUUID, Item.BaseData));
            // -->EventMessenger.Instance.Raise(new EventUIInventoryItemChanged(Item.BaseData.InventoryUniqueUUID, Item.BaseData));

            // We no longer need this references
            // Set back to a safe value 
            DraggedItem = null;
            DraggedItemStartSlot = null;
            _tmpItemStartPosition = transform.position;

            ToggleQuantity(true);
            ToggleDamageBar(true);
            UpdateQuantity();
            UpdateDamageBar();

            // --> EventMessenger.Instance.Raise(new EventUIInventoryItemDropedOnSlot(this));
        }
        #endregion
    }
}