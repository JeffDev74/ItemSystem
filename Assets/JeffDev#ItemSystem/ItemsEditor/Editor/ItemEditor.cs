using FPS.ItemSystem;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ItemSystem
{
    public partial class ItemEditor : EditorWindow
    {
        BaseItem tmpItem = null;
        BaseItem editItem = null;
        
        protected enum ItemEditorTab
        {
            WEAPON,
            AMMO,
            RESOURCE,
            MISC
        }

        protected ItemEditorTab activeTab = ItemEditorTab.AMMO;
        private ItemEditorTab tmpActiveTab = ItemEditorTab.MISC;

        List<BaseItem> dbItems = new List<BaseItem>();

        Color defaultFontColor = Color.white;
        Color defaultBGColor = Color.gray;

        [MenuItem("JEFFDEV/Custom Editor/Items Editor %#o")]
        public static void Init()
        {
            ItemEditor window = EditorWindow.GetWindow<ItemEditor>();


            window.minSize = new Vector2(900, 665);
            window.titleContent.text = "Items Editor";
            window.Show();
        }

        void OnEnable()
        {
            defaultFontColor = GUI.contentColor;
            defaultBGColor = GUI.backgroundColor;

            // Added reorderablelist here to avoid warning
            //reorderableList = new UnityEditorInternal.ReorderableList(new List<CustomProperty>(), typeof(CustomProperty), true, true, true, true);
            ResetEditor();
            LoadEntityTypeModel();
        }

        void LoadEntityTypeModel()
        {
            // Set CustomProperty variable to empty
            // This is just a precaution so we dont 
            // get am empty list when rendering the list
            //customProperties = new List<CustomProperty>();

            switch (activeTab)
            {
                case ItemEditorTab.WEAPON:
                    {
                        tmpItem = editItem = new WeaponItem();

                        dbItems.Clear();
                        foreach (BaseItem item in tmpItem.DBModel.Database<WeaponItem>())
                        {
                            dbItems.Add(item);
                        }


                        break;
                    }
                case ItemEditorTab.AMMO:
                    {
                        tmpItem = editItem = new AmmoItem();
                        
                        dbItems.Clear();
                        foreach (BaseItem item in tmpItem.DBModel.Database<AmmoItem>())
                        {
                            dbItems.Add(item);
                        }

                        
                        break;
                    }
                case ItemEditorTab.RESOURCE:
                    {
                        tmpItem = editItem = new ResourceItem();
                        
                        dbItems.Clear();
                        foreach (BaseItem item in tmpItem.DBModel.Database<ResourceItem>())
                        {
                            dbItems.Add(item);
                        }

                        
                        break;
                    }
                case ItemEditorTab.MISC:
                    {
                        //tmpItem = editItem = new MiscItem();
                        
                        //dbItems.Clear();
                        //foreach (BaseItem item in tmpItem.DBModel.Database<MiscItem>())
                        //{
                        //    dbItems.Add(item);
                        //}
                        break;
                    }
                
            }
            System.Threading.Thread.Sleep(100);
            tmpActiveTab = activeTab;
        }

        void OnGUI()
        {
            if (tmpActiveTab != activeTab)
            {
                state = DisplayState.NONE;
                LoadEntityTypeModel();
            }

            //Debug.Log("ACTIVE TAB " + activeTab);
            TopTabBar();
            GUILayout.BeginHorizontal();
            ListView();
            ItemDetails();
            GUILayout.EndHorizontal();
            BottomBar();
        }

        void ResetEditor()
        {
            _selectedItemIndex = -1;
            state = DisplayState.NONE;
            showNewItemDetails = false;
            tmpItem = null;
            editItem = null;
            LoadEntityTypeModel();
        }
    }
}

