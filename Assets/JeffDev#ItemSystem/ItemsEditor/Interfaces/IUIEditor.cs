using FPS.ItemSystem;
using UnityEngine;

namespace FPS.ItemSystem
{
    public interface IUIEditor
    {
        void OnUIEditorGUI(BaseItem item);
    }
}