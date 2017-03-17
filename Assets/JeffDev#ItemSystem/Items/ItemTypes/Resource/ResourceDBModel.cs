using FPS.ItemSystem;
using UnityEngine;

namespace ItemSystem
{
    public class ResourceDBModel : ScriptableObjectItemDB<ResourceItem>
    {
        public static ResourceDBModel LoadDb()
        {
            return Resources.Load("Databases/SO/Resource_Items_DB") as ResourceDBModel;
        }

        public ResourceItem CreateNew()
        {
            return CreateNew<ResourceItem>();
        }
        public override T CreateNew<T>()
        {
            return new ResourceItem() as T;
        }

        public ResourceItem FactoreItem(ResourceSData data, ResourceNSData nsData)
        {
            return FactoreItem<ResourceItem, ResourceSData, ResourceNSData>(data, nsData);
        }
        public override T FactoreItem<T, D, N>(D data, N nsData)
        {
            return new ResourceItem(data as ResourceSData, nsData as ResourceNSData) as T;
        }
    }

#if UNITY_EDITOR
    public class CreateResourceDataItemsDB : MonoBehaviour
    {
        [UnityEditor.MenuItem("JEFFDEV/Databases/Create Resource Items DB")]
        public static ResourceDBModel Create()
        {
            ResourceDBModel asset = ScriptableObject.CreateInstance<ResourceDBModel>();
            UnityEditor.AssetDatabase.CreateAsset(asset, "Assets/JeffDev#ItemSystem/ItemsEditor/Resources/Databases/SO/Resource_Items_DB.asset");
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.EditorUtility.FocusProjectWindow();
            UnityEditor.Selection.activeObject = asset;
            return asset;

        }
    }
#endif
}   // END NAMESPACE