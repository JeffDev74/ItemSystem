using System;
using FPS.ItemSystem;
using UnityEngine;

namespace FPS
{
    // This class will handle out custom item editor ui fields/display of data
    public partial class SData
    {
        public void OnUIEditorGUI(BaseItem item)
        {
            UnityEditor.EditorGUILayout.FloatField("Example", 56);
        }
    }
}