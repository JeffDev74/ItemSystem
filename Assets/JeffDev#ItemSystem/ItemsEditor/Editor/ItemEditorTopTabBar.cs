using UnityEditor;
using UnityEngine;

namespace ItemSystem
{
    public partial class ItemEditor
    {
        string searchString = "";
        EditorSortBy editorSortBy = EditorSortBy.ID;
        enum EditorSortBy
        {
            Name,
            ID
        }

        EditorOrderBy editorOrderBy = EditorOrderBy.ASC;
        enum EditorOrderBy
        {
            ASC,
            DESC,
        }

        bool editorAutoHideList = false;

        void TopTabBar()
        {
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
            
            AmmoTab();
            ResourcesTab();
            MiscTab();
            
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));

            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
            searchString = EditorGUILayout.TextField("Search", searchString);
            editorSortBy = (EditorSortBy)EditorGUILayout.EnumPopup("Sort By", editorSortBy);
            editorOrderBy = (EditorOrderBy)EditorGUILayout.EnumPopup("Order By", editorOrderBy);
            editorAutoHideList = EditorGUILayout.Toggle("Auto Hide List", editorAutoHideList);
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true));
            GUILayout.Label("something");
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

        }

        void AmmoTab()
        {
            if (activeTab == ItemEditorTab.AMMO) GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("AMMO"))
            {
                activeTab = ItemEditorTab.AMMO;
                ResetEditor();
                return;
            }
            GUI.backgroundColor = defaultBGColor;
        }

        void ResourcesTab()
        {
            if (activeTab == ItemEditorTab.RESOURCE) GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("RESOURCES"))
            {
                activeTab = ItemEditorTab.RESOURCE;
                ResetEditor();
                return;
            }
            GUI.backgroundColor = defaultBGColor;
        }

        void MiscTab()
        {
            if (activeTab == ItemEditorTab.MISC) GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("MISC ITEMS"))
            {
                activeTab = ItemEditorTab.MISC;
                ResetEditor();
                return;
            }
            GUI.backgroundColor = defaultBGColor;
        }
    }
}