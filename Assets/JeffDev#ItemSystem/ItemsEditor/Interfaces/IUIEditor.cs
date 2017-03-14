using FPS.ItemSystem;
using UnityEngine;

namespace ItemSystem
{
    public interface IUIEditor
    {
        void OnUIEditorGUI(BaseItem item);
    }
}