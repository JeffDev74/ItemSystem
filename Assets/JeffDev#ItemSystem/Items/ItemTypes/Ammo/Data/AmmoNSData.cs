using System;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public class AmmoNSData : NSData, IUIEditor
    {
        #region IUIEditor Interface implementation

        public override void OnUIEditorGUI(BaseItem item)
        {
            base.OnUIEditorGUI(item);
        }

        #endregion IUIEditor Interface implementation
    }
}