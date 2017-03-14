
#if UNITY_EDITOR
namespace FPS.ItemSystem
{
    // This class will handle out custom item editor ui fields/display of data
    public partial class SData : IUIEditor
    {
        public virtual void OnUIEditorGUI(BaseItem item)
        {
            Name =  UnityEditor.EditorGUILayout.TextField("Name", Name);
            Description = UnityEditor.EditorGUILayout.TextField("Description", Description);
            Type = (ItemTypeEnum)UnityEditor.EditorGUILayout.EnumPopup("Type", Type);
        }
    }
}
#endif