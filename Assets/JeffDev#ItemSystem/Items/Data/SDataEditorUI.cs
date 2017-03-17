
#if UNITY_EDITOR
namespace FPS.ItemSystem
{
    

    // This class will handle out custom item editor ui fields/display of data
    public partial class SData : IUIEditor
    {
        #region IStackable Interface
        [System.NonSerialized]
        public bool iStackableInterfaceToggle = false;
        #endregion IStackable Interface

        public virtual void OnUIEditorGUI(BaseItem item)
        {
            Name =  UnityEditor.EditorGUILayout.TextField("Name", Name);
            Description = UnityEditor.EditorGUILayout.TextField("Description", Description);
            Type = (ItemTypeEnum)UnityEditor.EditorGUILayout.EnumPopup("Type", Type);

            #region IStackable Interface

            IStackable iStackableInterface = item as IStackable;
            if (iStackableInterface != null)
            {
                var indent = UnityEditor.EditorGUI.indentLevel;
                UnityEditor.EditorGUILayout.Space();
                UnityEditor.EditorGUILayout.BeginVertical("Box");
                UnityEngine.GUIStyle boldFontStyle = UnityEditor.EditorStyles.foldout;
                UnityEngine.FontStyle previousStyle = boldFontStyle.fontStyle;
                boldFontStyle.fontStyle = UnityEngine.FontStyle.Bold;
                iStackableInterfaceToggle = UnityEditor.EditorGUILayout.Foldout(iStackableInterfaceToggle, "IStackable Interface", boldFontStyle);
                boldFontStyle.fontStyle = previousStyle;
                if (iStackableInterfaceToggle)
                {
                    UnityEditor.EditorGUI.indentLevel = 1;
                    iStackableInterface.IsStackable = UnityEditor.EditorGUILayout.Toggle("Is Stackable", iStackableInterface.IsStackable);
                    iStackableInterface.DestroyOnUse = UnityEditor.EditorGUILayout.Toggle("Destroy on Use", iStackableInterface.DestroyOnUse);
                    iStackableInterface.Quantity = UnityEditor.EditorGUILayout.IntField("Quantity", iStackableInterface.Quantity);
                    iStackableInterface.StackableMax = UnityEditor.EditorGUILayout.IntField("Stackable Max", iStackableInterface.StackableMax);
                }
                UnityEditor.EditorGUI.indentLevel = indent;
                UnityEditor.EditorGUILayout.EndVertical();
            }

            #endregion IStackable Interface
        }
    }
}
#endif