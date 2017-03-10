using FPS.InventorySystem;

namespace FPS
{
    public interface IItem
    {
        ISData Data { get; set; }
        INSData NSData { get; set; }

        //IRTData RTData { get; set; }

        IInventory Inventory { get; set; }
    }
}