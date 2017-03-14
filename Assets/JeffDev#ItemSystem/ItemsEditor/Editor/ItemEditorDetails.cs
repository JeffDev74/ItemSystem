using UnityEngine;
using UnityEditor;
using System.Globalization;

namespace ItemSystem
{
    public partial class ItemEditor
    {
        Vector2 _itemDetailViewScrollPos = Vector2.zero;

        enum DisplayState
        {
            NONE,
            DETAILS
        }

        DisplayState state = DisplayState.NONE;

        bool showNewItemDetails = false;

        void ItemDetails()
        {
            _itemDetailViewScrollPos = GUILayout.BeginScrollView(_itemDetailViewScrollPos, "Box", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            DisplayItemDetails();
            GUILayout.EndScrollView();
        }

        void DisplayItemDetails()
        {
            GUILayout.BeginVertical(GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            //EditorGUILayout.LabelField("State " + state.ToString());
            switch (state)
            {
                case DisplayState.DETAILS:
                    if (showNewItemDetails)
                    {
                        EditorGUILayout.LabelField(new GUIContent("Item DB ID: " + editItem.BaseData.ID));

                        IUIEditor iDataUIEditorGUI = editItem.BaseData as IUIEditor;
                        if (iDataUIEditorGUI != null)
                        {
                            // DISPLAY FIELDS FOR BASEDATA
                            iDataUIEditorGUI.OnUIEditorGUI(editItem);
                        }
                        else
                        {
                            Debug.Log("Item DATA [" + editItem.BaseData.Name + "] does not implement IUIEditor interface.");
                        }

                        IUIEditor iNSDataUIEditorGUI = editItem.BaseNSData as IUIEditor;
                        if (iNSDataUIEditorGUI != null)
                        {
                            // DISPLAY FIELDS FOR NSDATA
                            iNSDataUIEditorGUI.OnUIEditorGUI(editItem);
                        }
                        else
                        {
                            Debug.Log("Item NSDATA [" + editItem.BaseData.Name + "] does not implement IUIEditor interface.");
                        }
                    }
                    break;
                default:
                    break;
            }
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal("Box");
            DisplayButtons();
            GUILayout.EndHorizontal();

            // Needed to update FancyIcons()
            Repaint();
        }

        void DisplayButtons()
        {
            if (!showNewItemDetails)
            {
                GUI.backgroundColor = new Color(1.0f, 0.36078431372f, 0.83921568627f, 1.0f);
                //GUI.contentColor = Color.blue;
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                //Debug.Log(activeTab.ToString().Trim(new System.Char[] { ' ', '_', '-' }).ToLower());
                string title = textInfo.ToTitleCase("Create " + activeTab.ToString().Replace('_', ' ').ToLower());

                if (GUILayout.Button(title, GUILayout.MinHeight(30)))
                {
                    _selectedItemIndex = -1;

                    // Create new Item
                    editItem = tmpItem.FactoreNewItem(tmpItem.BaseData, tmpItem.BaseNSData);

                    showNewItemDetails = true;
                    state = DisplayState.DETAILS;
                }
                //GUI.contentColor = defaultFontColor;
                GUI.backgroundColor = defaultBGColor;
            }
            else
            {
                GUI.backgroundColor = Color.green;
                //GUI.contentColor = Color.green;
                if (GUILayout.Button("Save"))
                {
                    if (_selectedItemIndex == -1) // Is this a new item?
                    {
                        if (editItem.BaseData.ID == -1)
                        {
                            // This is a brand new item
                            editItem.DBModel.Add(editItem);
                        }
                    }
                    else
                    {
                        editItem.DBModel.UpdateById(editItem.BaseData.ID, editItem);
                    }


                    ResetEditor();
                    GUI.FocusControl("CancelButtonControlName");
                }

                //GUI.contentColor = defaultFontColor;
                GUI.backgroundColor = defaultBGColor;
                if (_selectedItemIndex != -1)
                {
                    GUI.backgroundColor = Color.red;
                    //GUI.contentColor = Color.red;
                    if (GUILayout.Button("Delete"))
                    {
                        GUI.color = Color.red;
                        if (EditorUtility.DisplayDialog("Delete",
                            "Are you sure that you want to delete [" + editItem.BaseData.Name + "]?",
                            "Delete",
                            "Cancel"))
                        {
                            //tmpItem.ReadDBModel.RemoveEntityById(tmpItem.ItemData.id);
                            editItem.DBModel.Remove(editItem);
                            ResetEditor();
                            GUI.FocusControl("CancelButtonControlName");
                        }
                    }
                    //GUI.contentColor = defaultFontColor;
                    GUI.backgroundColor = defaultBGColor;
                }

                GUI.backgroundColor = Color.grey;
                GUI.SetNextControlName("CancelButtonControlName");
                if (GUILayout.Button("Cancel"))
                {
                    ResetEditor();
                    GUI.FocusControl("CancelButtonControlName");
                }
                GUI.backgroundColor = defaultBGColor;
            }
        }
    }
}

