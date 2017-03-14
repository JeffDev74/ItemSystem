using FPS.InventorySystem;
using FPS.ItemSystem.CustomProperty;
using ItemSystem;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public class SData : ISData
    {
        [SerializeField]
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [SerializeField]
        private string _uniqueUUID;
        public string UniqueUUID
        {
            get { return _uniqueUUID; }
            set { _uniqueUUID = value; }
        }

        [SerializeField]
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [SerializeField]
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public ItemTypeEnum _itemType;
        public ItemTypeEnum Type
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        //IInventoryData
        [SerializeField]
        private string _inventoryUUID;
        public string InventoryUUID
        {
            get { return _inventoryUUID; }
            set { _inventoryUUID = value; }
        }

        [SerializeField]
        private int _slotID;
        public int SlotID
        {
            get { return _slotID; }
            set { _slotID = value; }
        }

        // IStackeableData
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        //ICustomPropertyData
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

        #region ISDeepClone<Data> implementation

        public virtual SData SDeepClone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (SData)formatter.Deserialize(ms);
            }
        }

        #endregion ISDeepClone<Data> implementation
    }
}