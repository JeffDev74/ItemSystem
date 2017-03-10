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

        protected IItem _item;
        public IItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                UpdateUI();
            }
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
        private bool IsActive
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
                Icon.sprite = Item.NSData.Icon;
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
                QuantityText.text = Item.Data.Quantity.ToString();
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            DraggedItem = this;
            DraggedItemStartSlot = GetSlot();
            _tmpItemStartPosition = TheTransform.position;

            TheTransform.SetParent(MainCanvas.transform);

            TheCanvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (TheTransform.parent == DraggedItemStartSlot)
            {
                TheTransform.position = _tmpItemStartPosition;
            }

            UISlot tmpSlot = GetSlot(true);

            if (tmpSlot == null)
            {
                TheTransform.SetParent(DraggedItemStartSlot.transform);
                // Remove item from inventory

                if (Item != null)
                {
                    EventSystem.EventMessenger.Instance.Raise(new Events.EventRemoveItemFromInventory(DraggedItemStartSlot.InventoryUUID, DraggedItemStartSlot.ThisUIItem.Item, false));
                }

                Item = null;
                UpdateUI();

                // check quantity ??

                // Event item was removed...
            }
            else
            {
                if (tmpSlot.InventoryUUID != DraggedItemStartSlot.InventoryUUID)
                {
                    EventSystem.EventMessenger.Instance.Raise(new Events.EventAddItemToInventory(tmpSlot.InventoryUUID, tmpSlot.ThisUIItem.Item, false));
                }
            }




            // Set Variables back to a safe value
            DraggedItem = null;
            DraggedItemStartSlot = null;
            _tmpItemStartPosition = TheTransform.position;
            TheCanvasGroup.blocksRaycasts = true;
        }
    }
}