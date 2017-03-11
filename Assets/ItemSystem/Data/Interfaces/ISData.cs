using FPS.ItemSystem;
using FPS.ItemSystem.CustomProperty;
using UnityEngine;

namespace FPS
{
    public interface ISData
    {
        int ID { get; set; }
        string UniqueUUID { get; set; }
        string Name { get; set; }

        string Description { get; set; }

        ItemTypeEnum Type { get; set; }

        // IInventoryData
        string InventoryUUID { get; set; }
        int SlotID { get; set; }

        // ICustomPropertyData
        PropertyManager Properties { get; set; }

        //IStackable
        int Quantity { get; set; }
    }
}