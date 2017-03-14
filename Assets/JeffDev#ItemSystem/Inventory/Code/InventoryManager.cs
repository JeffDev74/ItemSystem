using System.Collections.Generic;
using UnityEngine;

namespace FPS.InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        public ItemSystem.AmmoSData theAmmo;

        // Refactore code to check and remove any othe
        // inventory manager found in scene
        private static InventoryManager _instance;
        public static InventoryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<InventoryManager>();
                }
                return _instance;
            }
        }

        List<IInventory> _inventories;
        List<IInventory> Inventories
        {
            get
            {
                if (_inventories == null)
                {
                    _inventories = new List<IInventory>();
                }
                return _inventories;
            }
        }

        [SerializeField]
        private Inventory _mainInventory;
        public IInventory MainInventory
        {
            get { return _mainInventory as IInventory; }
            set { _mainInventory = value as Inventory; }
        }

        [SerializeField]
        private Inventory _actionBarInventory;
        public IInventory ActionBarInventory
        {
            get { return _actionBarInventory as IInventory; }
            set { _actionBarInventory = value as Inventory; }
        }

        private void Awake()
        {
            Inventories.Clear();
            AddInventory(MainInventory);
            AddInventory(ActionBarInventory);
        }

        public void AddInventory(IInventory inv)
        {
            if (GetInventoryByUUID(inv.InventoryUUID) == null)
            {
                Debug.Log("Adding inventory [" + inv.InventoryUUID + "] to the inventory manager");
                Inventories.Add(inv);
            }
        }

        public void RemoveInventory(IInventory inv)
        {

        }

        public IInventory GetInventoryByUUID(string inventoryUniqueUUID)
        {
            if (string.IsNullOrEmpty(inventoryUniqueUUID))
            {
                Debug.LogWarning("Inventory unique UUID is empty. Returning null.", transform);
                return null;
            }

            for (int i = 0; i < Inventories.Count; i++)
            {
                if (Inventories[i].InventoryUUID == inventoryUniqueUUID)
                {
                    return Inventories[i];
                }
            }

            Debug.LogWarning("The inventory with unique uuid [" + inventoryUniqueUUID + "] was not found.", transform);

            return null;
        }

        public void SetPlayerInventory(IInventory inventory)
        {
            MainInventory = inventory;
        }

        public void SetPlayerActionBarInventory(IInventory inventory)
        {
            ActionBarInventory = inventory;
        }
    }
}