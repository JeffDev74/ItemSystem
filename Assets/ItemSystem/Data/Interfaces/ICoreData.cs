using FPS.InventorySystem;
using UnityEngine;

namespace FPS.ItemSystem
{
	public interface ICoreData
	{
        SData BaseData { get; set; }
        NSData BaseNSData { get; set; }

        IInventory Inventory { get; set; }
    }
}