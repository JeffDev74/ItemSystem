using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public class AmmoSData : SData, ISerializeData, ISData, IUIEditor
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
            string orgitemName = Name;
            string originalUUID = UniqueUUID;
            int originalSlotID = SlotID;

            AmmoItem factoredItem = new AmmoItem();

            factoredItem.BaseData.Name = orgitemName;
            factoredItem.BaseData.UniqueUUID = originalUUID;
            factoredItem.BaseData.SlotID = originalSlotID;
            
            return factoredItem;
        }

        #endregion ISerializeData Implementation

        public void OnUIEditorGUI(BaseItem item)
        {
            Debug.Log("hit");
        }
    }
}