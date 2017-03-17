using UnityEngine;

namespace FPS.ItemSystem
{
	public interface IStackable : IStackableData
    {
        StackResult Stack(BaseItem rightItem);
    }

    /// <summary>
    /// This class is used to hold the items that are being 
    /// stacked when user is working on inventory. It acts as
    /// a temporary object holder for the items being stacked
    /// </summary>
    public class StackResult
    {
        public BaseItem item;
    }
}