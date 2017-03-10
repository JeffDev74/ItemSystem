using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public class AmmoSData : SData, ISerializeData
	{
        public float TravelDistance;

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
            string originalUUID = ItemUUID;
            int originalSlotID = InventorySlotId;

            AmmoItem factoredItem = new AmmoItem();

            factoredItem.BaseData.ItemUUID = originalUUID;
            factoredItem.BaseData.InventorySlotId = originalSlotID;
            
            return factoredItem;
        }
        #endregion ISerializeData Implementation
    }
}