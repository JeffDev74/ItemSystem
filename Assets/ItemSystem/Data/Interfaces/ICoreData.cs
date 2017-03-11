using FPS.InventorySystem;

namespace FPS.ItemSystem
{
	public interface ICoreData
	{
        ISData BaseData { get; set; }
        INSData BaseNSData { get; set; }

        IInventory Inventory { get; set; }
    }
}