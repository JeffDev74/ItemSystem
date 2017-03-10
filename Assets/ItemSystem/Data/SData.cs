using FPS.InventorySystem;
using FPS.ItemSystem.CustomProperty;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public class SData
    {
        public int ID;
        public string ItemUUID;
        public int InventorySlotId;
        public string ItemName;
        public string Description;
        public ItemTypeEnum ItemType;

        //IInventoryData
        public string InventoryUUID;

        // IStackeableData
        public int Quantity;

        [SerializeField]
        public PropertyManager _properties;
        public PropertyManager Properties
        {
            get
            {
                if(_properties == null)
                {
                    _properties = new PropertyManager();
                }
                return _properties;
            }
            set { _properties = value; }
        }
	}
}