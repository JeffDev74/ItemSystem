using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using FPS.ItemSystem;
using FPS;

namespace ItemSystem
{
    public abstract class ScriptableObjectItemDB<I> : ScriptableObject, IDBModel where I : BaseItem
    {
        [SerializeField]
        protected List<I> database = new List<I>();

        [SerializeField]
        protected int index;

        public int Count
        {
            get
            {
                return database.Count;
            }
        }

        public static SO LoadDb<SO>(string DBPath) where SO : ScriptableObject
        {
            SO resource = Resources.Load(DBPath) as SO;
            if (resource == null) { Debug.LogError("Failed to load scripted object database from [" + DBPath + "]"); }
            return resource;
        }

        public void Add<T>(T item) where T : BaseItem
        {
            AddGetId<T>(item);
        }

        public int AddGetId<T>(T item) where T : BaseItem
        {
            ++index;
            item.BaseData.ID = index;
            database.Add(item as I);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
            return item.BaseData.ID;
        }

        public virtual T CreateNew<T>() where T : BaseItem
        {
            Debug.LogError("Implement CreateNew method (override) in the respective database model implementation.");
            return null;
        }

        public List<T> Database<T>() where T : BaseItem
        {
            //return database as List<T>;
            return database.Cast<T>().ToList();
        }

        public virtual T FactoreItem<T, D, N>(D data, N nsData)
            where T : BaseItem
            where D : ISData
            where N : NSData
        {
            Debug.LogError("Implement FactoreItem method (override) in the respective database model implementation.");
            return null;
        }

        public T GetById<T>(int id) where T : BaseItem
        {
            var itemx = database.Find(delegate (I i) { return i.BaseData.ID == id; });
            if (itemx != null)
            {
                var item = itemx.ShallowCopy() as I;
                item.BaseData = item.BaseData.SDeepClone();
                item.BaseNSData = item.BaseNSData.NSDeepClone();
                if (item != null)
                {
                    item.BaseData.UniqueUUID = string.Empty;
                    item.BaseData.InventoryUUID = string.Empty;
                    item.BaseData.SlotID = -1;
                    //item.BaseData.InventorySlotName = string.Empty;
                }
                return item as T;
                //return database.Find(delegate (I i) { return i.BaseData.ID == id; }) as T;
            }

            Debug.LogError("Could not find Item by id [" + id + "]. Does the item have the database id set in the inspector?");
            return null;
        }

        public T GetByName<T>(string name) where T : BaseItem
        {
            //var item = database.Find(delegate (I i) { return i.BaseData.ItemName == name; });
            //item = item.ShallowCopy() as I;
            //if (item != null)
            //{
            //    item.BaseData.UniqueUUID = string.Empty;
            //    item.BaseData.InventoryUniqueUUID = string.Empty;
            //    item.BaseData.InventorySlotId = -1;
            //    item.BaseData.InventorySlotName = string.Empty;
            //}
            //return item as T;
            return database.Find(delegate (I i) { return i.BaseData.Name == name; }) as T;
        }

        public T GetByIndex<T>(int index) where T : BaseItem
        {
            //var item = database.ElementAt(index);
            //item = item.ShallowCopy() as I;
            //if (item != null)
            //{
            //    item.BaseData.UniqueUUID = string.Empty;
            //    item.BaseData.InventoryUniqueUUID = string.Empty;
            //    item.BaseData.InventorySlotId = -1;
            //    item.BaseData.InventorySlotName = string.Empty;
            //}
            //return item as T;
            return database.ElementAt(index) as T;
        }

        public void Remove<T>(T item) where T : BaseItem
        {
            //database.Remove(item as I);
            RemoveById(item.BaseData.ID);
        }

        public void RemoveById(int id)
        {
            int index = -1;
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].BaseData.ID == id)
                {
                    index = database.IndexOf(database[i]);
                }
            }

            if (index > -1)
            {
                RemoveByIndex(index);
            }
        }

        public void RemoveByIndex(int index)
        {
            database.RemoveAt(index);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public void SortById()
        {
            database.Sort(delegate (I first, I second) { if (first.BaseData.ID > second.BaseData.ID) return 1; else if (first.BaseData.ID == second.BaseData.ID) return 0; else return -1; });
        }

        public void SortByName()
        {
            database.Sort(delegate (I first, I second) { return System.String.Compare(first.BaseData.Name, second.BaseData.Name); });
        }

        public void UpdateItem<T>(T item) where T : BaseItem
        {
            UpdateById<T>(item.BaseData.ID, item);
        }

        public void UpdateById<T>(int id, T item) where T : BaseItem
        {
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].BaseData.ID == id)
                {
                    item.BaseData.ID = id;
                    UpdateByIndex<T>(database.IndexOf(database[i]), item);
                    break;
                }
            }
        }

        public void UpdateByIndex<T>(int index, T item) where T : BaseItem
        {
            database[index] = item as I;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}