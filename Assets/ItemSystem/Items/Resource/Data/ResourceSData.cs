using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public class ResourceSData : SData, ISerializeData
	{
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
            string orgitemName = ItemName;
            string originalUUID = ItemUUID;
            int originalSlotID = InventorySlotId;

            ResourceItem factoredItem = new ResourceItem();

            factoredItem.BaseData.ItemName = orgitemName;
            factoredItem.BaseData.ItemUUID = originalUUID;
            factoredItem.BaseData.InventorySlotId = originalSlotID;

            return factoredItem;
        }
        #endregion ISerializeData Implementation
    }
}