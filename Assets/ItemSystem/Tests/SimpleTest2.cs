using FPS.ItemSystem;
using FPS.ItemSystem.CustomProperty;
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

        private SQLiteItemDBModel _dBModel;
        private SQLiteItemDBModel DBModel
        {
            get
            {
                if(_dBModel == null)
                {
                    _dBModel = SQLiteItemDBModel.Instance;
                }

                return _dBModel;
            }
        }

        private void Start()
        {
            BaseItem[] items = DBModel.GetAllItems();

            Debug.Log(items.Length);

            foreach (var item in items)
            {
                Debug.Log("Item [" + item.BaseData.ItemName + "] was loaded.");
            }
        }

        public void TestCreateItemsAndSaveToSqlite()
        {
            ResourceItem = new ResourceItem();
            ResourceItem.BaseData.ItemName = "Wood";
            ResourceItem.BaseData.ItemUUID = System.Guid.NewGuid().ToString();

            StringProperty myString = new StringProperty("some_key", "This is a custom string property");

            ResourceItem.BaseData.Properties.Add(myString);

            Debug.Log("The original value is [" + ResourceItem.BaseData.Properties.Get<string>("some_key") + "]");

            ResourceItem.BaseData.Properties.Set("some_key", "This is the updated value of a custom property");

            string propValue = ResourceItem.BaseData.Properties.Get<string>("some_key");
            Debug.Log("This is the value [" + propValue + "]");

            Bullet = new AmmoItem();
            Bullet.BaseData.ItemName = "9mm";
            Bullet.BaseData.ItemUUID = System.Guid.NewGuid().ToString();
            (Bullet.BaseData as AmmoSData).TravelDistance = 100;

            Items.Add(Bullet);
            Items.Add(ResourceItem);

            foreach (BaseItem item in Items)
            {
                Debug.Log("The item name is [" + item.BaseData.ItemName + "]");
                DBModel.CreateItem(item);
            }
        }
    }
}