using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public partial class WeaponSData : SData, ISerializeData, ISData, IStackableData
    {
        [SerializeField]
        private int _bulletsLeft;
        public int BulletsLeft
        {
            get { return _bulletsLeft; }
            set { _bulletsLeft = value; }
        }

        #region IStackableData Interface Implementation

        [SerializeField]
        private bool _isStackable;
        public bool IsStackable
        {
            get { return _isStackable; }
            set { _isStackable = value; }
        }

        [SerializeField]
        private bool _destroyOnUse;
        public bool DestroyOnUse
        {
            get { return _destroyOnUse; }
            set { _destroyOnUse = value; }
        }

        [SerializeField]
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        [SerializeField]
        private int _stackableMax;
        public int StackableMax
        {
            get { return _stackableMax; }
            set { _stackableMax = value; }
        }

        #endregion IStackableData Interface Implementation

        #region ISerializeData Implementation

        public string SerializeItemData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            using (memoryStream)
            {
                formatter.Serialize(memoryStream, this);
            }
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public BaseItem FactoryCloneItemFromData()
        {
            string orgitemName = Name;
            string originalUUID = UniqueUUID;
            int originalSlotID = SlotID;

            WeaponItem factoredItem = new WeaponItem();

            factoredItem.BaseData.Name = orgitemName;
            factoredItem.BaseData.UniqueUUID = originalUUID;
            factoredItem.BaseData.SlotID = originalSlotID;

            return factoredItem;
        }

        #endregion ISerializeData Implementation
    }
}