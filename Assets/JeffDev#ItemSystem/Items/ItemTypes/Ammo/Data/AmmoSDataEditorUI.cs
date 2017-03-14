
#if UNITY_EDITOR
namespace FPS.ItemSystem
{
    public partial class AmmoSData : IUIEditor
    {
        #region IUIEditor Interface Implementation
        public override void OnUIEditorGUI(BaseItem item)
        {
            base.OnUIEditorGUI(item);
            TravelDistance = UnityEditor.EditorGUILayout.FloatField("Travel Distance", TravelDistance);
        }
        #endregion IUIEditor Interface Implementation
    }
}
#endif