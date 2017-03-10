using FPS.ItemSystem;
using FPS.ItemSystem.CustomProperty;
using UnityEngine;

namespace FPS
{
    public interface ISData
    {
        int ID { get; set; }
        string ItemUUID { get; set; }
        string ItemName { get; set; }
        ItemTypeEnum ItemType { get; set; }

        // IInventoryData
        int SlotId { get; set; }

        // ICustomPropertyData
        PropertyManager Properties { get; set; }

        //IStackable
        int Quantity { get; set; }
    }
}