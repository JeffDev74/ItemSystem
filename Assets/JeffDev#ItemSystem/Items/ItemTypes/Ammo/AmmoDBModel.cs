using FPS.ItemSystem;
using UnityEngine;

namespace ItemSystem
{
    //[System.Serializable]
    public class AmmoDBModel : ScriptableObjectItemDB<AmmoItem>
    {
        public static AmmoDBModel LoadDb()
        {
            return ScriptableObjectItemDB<AmmoItem>.LoadDb<AmmoDBModel>("Databases/SO/Ammo_Items_DB") as AmmoDBModel;
            //return LoadDb<AmmoDBModel>("Databases/SO/Ammo_Items_DB") as AmmoDBModel;
        }

        public AmmoItem CreateNew()
        {
            return CreateNew<AmmoItem>();
        }
        public override T CreateNew<T>()
        {
            return new AmmoItem() as T;
        }

        public AmmoItem FactoreItem(AmmoSData data, AmmoNSData nsData)
        {
            return FactoreItem<AmmoItem, AmmoSData, AmmoNSData>(data, nsData);
        }
        public override T FactoreItem<T, D, N>(D data, N nsData) // T = ResourceItem, AmmoItem etc. D is BaseData N is NSData
        {
            return new AmmoItem(data as AmmoSData, nsData as AmmoNSData) as T;
        }
    }

#if UNITY_EDITOR
    public class CreateAmmoDataItemsDB : MonoBehaviour
    {
        [UnityEditor.MenuItem("JEFF/Databases/Create Ammo Items DB")]
        public static AmmoDBModel Create()
        {
            AmmoDBModel asset = ScriptableObject.CreateInstance<AmmoDBModel>();
            UnityEditor.AssetDatabase.CreateAsset(asset, "Assets/#ItemsCustomEditor/Resources/Databases/SO/Ammo_Items_DB.asset");
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.EditorUtility.FocusProjectWindow();
            UnityEditor.Selection.activeObject = asset;
            return asset;
        }
    }
#endif
}   // END NAMESPACE