using FPS;
using FPS.ItemSystem;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public interface IDBModel
    {
        int Count { get; }

        List<T> Database<T>() where T : BaseItem;

        T GetById<T>(int id) where T : BaseItem;

        T GetByIndex<T>(int index) where T : BaseItem;

        T CreateNew<T>() where T : BaseItem;

        T FactoreItem<T, D, N>(D data, N nsData) where T : BaseItem where D : ISData where N : NSData;

        void Add<T>(T item) where T : BaseItem;

        int AddGetId<T>(T item) where T : BaseItem;

        void UpdateItem<T>(T item) where T : BaseItem;

        void UpdateById<T>(int id, T item) where T : BaseItem;

        void UpdateByIndex<T>(int index, T item) where T : BaseItem;

        void Remove<T>(T item) where T : BaseItem;

        void RemoveById(int id);

        void RemoveByIndex(int index);

        void SortByName();

        void SortById();
    }
}