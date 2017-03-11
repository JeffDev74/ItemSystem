using FPS.InventorySystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
	public interface INSData
	{
        Sprite Icon { get; set; }

        //IInventoryNSData
        UISlot Slot { get; set; }
	}
}