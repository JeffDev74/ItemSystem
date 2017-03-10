using UnityEngine;

namespace FPS.UI
{
    public interface IUIPanel
    {
        Transform TheTransform { get; }
        int ID { get; }
        string PanelName { get; }
        string PanelUniqueUUID { get; set; }
        bool IsActive { get; }
        void TogglePanel(bool state);
    }
}