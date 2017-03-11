using FPS.InventorySystem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        
        private List<IUIPanel> _panels;
        public List<IUIPanel> Panels
        {
            get
            {
                if(_panels == null)
                {
                    _panels = new List<IUIPanel>();
                }

                return _panels;
            }
            set { _panels = value; }
        }

        [SerializeField]
        private UIInventory _mainInventoryUIPanel;
        public IUIPanel MainInventoryUIPanel
        {
            get { return _mainInventoryUIPanel as IUIPanel; }
            set { _mainInventoryUIPanel = value as UIInventory; }
        }

        [SerializeField]
        private UIInventory _actionBarUIPanel;
        private IUIPanel ActionBarUIPanel
        {
            get { return _actionBarUIPanel as IUIPanel; }
            set { _actionBarUIPanel = value as UIInventory; }
        }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            GameObject[] _goPanels = GameObject.FindGameObjectsWithTag(Helper.ObjectTags.UIPanel);
            for (int i = 0; i < _goPanels.Length; i++)
            {
                IUIPanel p = _goPanels[i].GetComponent<IUIPanel>();
                if(p != null)
                {
                    AddPanel(p);
                }
            }
        }

        public IUIPanel GetPanelByName(string panelName)
        {
            for (int i = 0; i < Panels.Count; i++)
            {
                if(Panels[i].PanelName == panelName)
                {
                    return Panels[i];
                }
            }

            Debug.LogWarning("The panel [" + panelName + "] is not registered with UIManager.", transform);
            return null;
        }

        public void AddPanel(IUIPanel panel)
        {
            if(GetPanelByName(panel.PanelName) == null)
            {
                Panels.Add(panel);
            }
            else
            {
                Debug.Log("The panel [" + panel.PanelName + "] is already registered with the UIManager.");
            }
        }
    }
}