using FPS.ItemSystem;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
	public class SimpleTest2 : MonoBehaviour
	{
        public ResourceItem ResourceItem;
        public AmmoItem Bullet;

        List<BaseItem> _items;
        List<BaseItem> Items
        {
            get
            {
                if(_items == null)
                {
                    _items = new List<BaseItem>();
                }
                return _items;
            }
            set { _items = value; }
        }

        private void Start()
        {
            ResourceItem = new ResourceItem();
            ResourceItem.BaseData.ItemName = "Wood";
            ResourceItem.BaseData.ItemUUID = System.Guid.NewGuid().ToString();
            (ResourceItem.BaseData as ResourceSData).some = 10;

            Bullet = new AmmoItem();
            Bullet.BaseData.ItemName = "9mm";
            Bullet.BaseData.ItemUUID = System.Guid.NewGuid().ToString();
            (Bullet.BaseData as AmmoSData).TravelDistance = 100;

            Items.Add(Bullet);
            Items.Add(ResourceItem);

            foreach (BaseItem item in Items)
            {
                Debug.Log("The item name is [" + item.BaseData.ItemName + "]");
            }
        }
    }
}