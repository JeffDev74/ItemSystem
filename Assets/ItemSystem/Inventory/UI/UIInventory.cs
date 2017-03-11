
using UnityEngine;
using FPS.EventSystem;

using FPS.UI;
using FPS.InventorySystem.Events;
using FPS.ItemSystem;

namespace FPS.InventorySystem.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIInventory : MonoBehaviour, IUIPanel
    {
        private int _id = -1;
        public int ID
        {
            get { return _id; }
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
        }

        [SerializeField]
        private string _panelName = "Unnamed";
        public string PanelName
        {
            get { return _panelName; }
        }

        public string _panelUniqueUUID;
        public string PanelUniqueUUID
        {
            get { return _panelUniqueUUID; }
            set { _panelUniqueUUID = value; }
        }

        private Transform _theTransform;
        public Transform TheTransform
        {
            get
            {
                if (_theTransform == null)
                {
                    _theTransform = transform;
                }
                return _theTransform;
            }
        }

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

        [SerializeField]
        private UISlotsContainer _theSlotsContainer;
        private UISlotsContainer TheSlotsContainer
        {
            get
            {
                if (_theSlotsContainer == null)
                {
                    _theSlotsContainer = TheTransform.GetComponentInChildren<UISlotsContainer>();
                }
                return _theSlotsContainer;
            }
        }

        public string _inventoryUUID;
        public string InventoryUUID
        {
            get { return _inventoryUUID; }
            set { _inventoryUUID = value; }
        }

        [SerializeField]
        private Inventory _theInventory;
        public IInventory TheInventory
        {
            get
            {
                if (_theInventory == null)
                {
                    _theInventory = InventoryManager.Instance.GetInventoryByUUID(InventoryUUID) as Inventory;
                }

                return _theInventory as IInventory;
            }
            set { _theInventory = value as Inventory; }
        }

        public void TogglePanel(bool state)
        {
            TheCanvasGroup.alpha = state ? 1 : 0;
            TheCanvasGroup.interactable = state;
            TheCanvasGroup.blocksRaycasts = state;
            // Are we going to animate the panels??
            // AnimatePanel(state)
        }

        protected void OnEnable()
        {
            EventMessenger.Instance.AddListner<EventItemWasAddedToInventory>(OnItemAddedToInventory);
        }

        protected void OnDisable()
        {
            EventMessenger.Instance.RemoveListner<EventItemWasAddedToInventory>(OnItemAddedToInventory);
        }

        protected void Start()
        {
            SetupSlots();
        }

        private void SetupSlots()
        {
            // Set the slot inventory
            for (int i = 0; i < TheSlotsContainer.SlotsCount; i++)
            {
                // NOTE: if inventory is not set on start
                // the slots must be updated at some point
                UISlot slot = TheSlotsContainer.UISlotList[i];
                slot.InventoryUUID = InventoryUUID;
            }
        }

        private void OnItemAddedToInventory(EventItemWasAddedToInventory e)
        {
            if (e.UpdateUI == false)
            {
                Debug.Log("received event on ui but we should not update");
                return;
            }

            if (InventoryUUID == e.InventoryUUID && e.UpdateUI)
            {
                AddItem(e.Item, e.UpdateUI);
            }
        }

        private void AddItem(ICoreData item, bool updateUI)
        {
            if (TheInventory == null)
            {
                Debug.LogWarning("The inventory is null... Is this a network ui object??");
                return;
            }

            UISlot slot = TheSlotsContainer.GetSlot();
            if (slot)
            {
                slot.UpdateSlotItem(item, updateUI);
            }
            else
            {
                Debug.LogError("The slot is null.. is the inventory full???");
            }
        }
    }
}