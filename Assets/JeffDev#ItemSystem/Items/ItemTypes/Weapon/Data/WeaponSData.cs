using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public partial class WeaponSData : SData, ISerializeData, ISData
    {
        [SerializeField]
        private int _bulletsLeft;
        public int BulletsLeft
        {
            get { return _bulletsLeft; }
            set { _bulletsLeft = value; }
        }

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