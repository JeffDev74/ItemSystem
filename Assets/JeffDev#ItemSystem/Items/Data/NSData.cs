using FPS.InventorySystem.UI;
using ItemSystem;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public class NSData : INSData
    {
        private Sprite _icon;
        public Sprite Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        private UISlot _slot;
        public UISlot Slot
        {
            get { return _slot; }
            set { _slot = value; }
        }

        public NSData NSDeepClone()
        {
            return (NSData)this.MemberwiseClone();
        }
    }
}