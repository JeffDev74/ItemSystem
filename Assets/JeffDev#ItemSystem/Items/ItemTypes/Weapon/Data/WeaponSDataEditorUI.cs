
#if UNITY_EDITOR
namespace FPS.ItemSystem
{
	public partial class WeaponSData : IUIEditor
    {
        #region IUIEditor Interface Implementation
        public override void OnUIEditorGUI(BaseItem item)
        {
            base.OnUIEditorGUI(item);
            BulletsLeft = UnityEditor.EditorGUILayout.IntField("Bullets Left", BulletsLeft);
        }
        #endregion IUIEditor Interface Implementation
    }
}
#endif