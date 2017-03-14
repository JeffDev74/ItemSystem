using FPS.InventorySystem.UI;
using FPS.ItemSystem;
using ItemSystem;
using UnityEngine;


namespace FPS
{
    // Non Serializable data
	public interface INSData : INSDeepClone<NSData>
    {
        Sprite Icon { get; set; }

        //IInventoryNSData
        UISlot Slot { get; set; }
    }
}