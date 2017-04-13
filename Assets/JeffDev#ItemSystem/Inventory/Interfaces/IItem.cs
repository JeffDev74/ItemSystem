using FPS.ItemSystem;
using UnityEngine;

namespace FPS
{
    public interface IItem
    {
        //ISData SData { get; set; }

        //INSData NSData { get; set; }

        //IInventory Inventory { get; set; }

        Transform TheTransform { get; set; }
        BaseItem Item { get; set; }

        //ICoreData CoreData { get; set; }
    }
}