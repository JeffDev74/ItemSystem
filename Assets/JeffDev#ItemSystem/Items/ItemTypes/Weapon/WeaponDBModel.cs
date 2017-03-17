using FPS.ItemSystem;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class WeaponDBModel : ScriptableObjectItemDB<WeaponItem>
    {
        public static WeaponDBModel LoadDb()
        {
            return ScriptableObjectItemDB<WeaponItem>.LoadDb<WeaponDBModel>("Databases/SO/Weapon_Items_DB") as WeaponDBModel;
        }

        public WeaponItem CreateNew()
        {
            return CreateNew<WeaponItem>();
        }
        public override T CreateNew<T>()
        {
            return new WeaponItem() as T;
        }

        public WeaponItem FactoreItem(WeaponSData data, WeaponNSData nsData)
        {
            return FactoreItem<WeaponItem, WeaponSData, WeaponNSData>(data, nsData);
        }
        public override T FactoreItem<T, D, N>(D data, N nsData) // T = ResourceItem, AmmoItem etc. D is BaseData N is NSData
        {
            return new WeaponItem(data as WeaponSData, nsData as WeaponNSData) as T;
        }
    }

#if UNITY_EDITOR
    public class CreateWeaponDataItemsDB : MonoBehaviour
    {
        [UnityEditor.MenuItem("JEFFDEV/Databases/Create Weapon Items DB")]
        public static WeaponDBModel Create()
        {
            WeaponDBModel asset = ScriptableObject.CreateInstance<WeaponDBModel>();
            UnityEditor.AssetDatabase.CreateAsset(asset, "Assets/JeffDev#ItemSystem/ItemsEditor/Resources/Databases/SO/Weapon_Items_DB.asset");
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.EditorUtility.FocusProjectWindow();
            UnityEditor.Selection.activeObject = asset;
            return asset;
        }
    }
#endif
}   // END NAMESPACE