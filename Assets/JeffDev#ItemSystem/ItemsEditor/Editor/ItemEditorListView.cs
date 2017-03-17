using UnityEngine;
using System.Collections.Generic;
using FPS.ItemSystem;

namespace ItemSystem
{
    public partial class ItemEditor
    {
        Vector2 _lisrViewScrollPos = Vector2.zero;
        int listViewWidth = 180;
        //int _listViewButtonWidth = 180;
        //int _listViewButtonHeight = 25;
        int showingItemsCount = 0;
        int totalItemsCount = 0;

        int _selectedItemIndex = -1;
        void ListView()
        {
            if (editorAutoHideList)
            {
                if (state != DisplayState.NONE)
                {
                    return;
                }
            }

            _lisrViewScrollPos = GUILayout.BeginScrollView(_lisrViewScrollPos, "Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true), GUILayout.MaxWidth(listViewWidth)); //GUILayout.MaxWidth(listViewWidth)
            ItemListView();
            GUILayout.EndScrollView();
        }

        private void ItemListView()
        {
            if (tmpItem == null)
            {
                return;
            }

            if (tmpItem.DBModel != null)
            {
                // if we are switching tabs 
                // return to avoid null references
                if (tmpActiveTab != activeTab)
                {
                    return;
                }

                
                var items = dbItems; 

                if (items == null)
                {
                    Debug.LogError("This was working fine with unity 5.4. For some reason its not working with unity 5.5");

                    return;
                }


                totalItemsCount = items.Count;

                // UNCOMMENT AFTER MOVE THE FILTER TO THE DBMODEL
                // Apply the topbar filter values
                items = ApplyTopBarFilters(items);

                showingItemsCount = items.Count;

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] == null)
                    {
                        Debug.LogError("Got a null data value here here!");
                        return;
                    }
                    if (items[i].BaseData == null)
                    {
                        continue;
                    }

                    if (editItem != null && items[i].BaseData.ID == editItem.BaseData.ID) GUI.backgroundColor = Color.cyan;
                    GUILayout.BeginVertical("Box", GUILayout.MinWidth(100), GUILayout.ExpandWidth(true), GUILayout.MaxWidth(listViewWidth));

                    GUI.backgroundColor = defaultBGColor;
                    if (GUILayout.Button(items[i].BaseNSData.Icon.texture, GUILayout.MinWidth(100), GUILayout.ExpandWidth(true), GUILayout.Height(90), GUILayout.MaxWidth(listViewWidth)))
                    {
                        ResetEditor();
                        _selectedItemIndex = i;

                        // Keep the model but load the data
                        // on the entity
                        editItem = tmpItem.FactoreNewItem(items[i].BaseData, items[i].BaseNSData);

                        showNewItemDetails = true;
                        state = DisplayState.DETAILS;
                    }
                    GUILayout.Button(items[i].BaseData.Name + " NS Data", GUILayout.MinWidth(100), GUILayout.ExpandWidth(true), GUILayout.MaxWidth(listViewWidth));
                    GUILayout.Button(items[i].BaseData.Name + " Property Data", GUILayout.MinWidth(100), GUILayout.ExpandWidth(true), GUILayout.MaxWidth(listViewWidth));
                    GUILayout.EndVertical();
                }
            }
            else
            {
                Debug.LogError("Item DBModel is null.");
            }
        }

        // THIS MUST BE MOVED TO THE ENTITY DB_MODEL
        public List<D> ApplyTopBarFilters<D>(List<D> items) where D : BaseItem
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.FindAll(x => x.BaseData.Name.ToLower().Contains(searchString.ToLower()));
            }

            if (editorSortBy == EditorSortBy.ID)
            {
                items.Sort(
                    delegate (D first, D second)
                    {
                        if (first.BaseData.ID > second.BaseData.ID)
                        {
                            // return 1
                            return editorOrderBy == EditorOrderBy.ASC ? 1 : -1;
                        }
                        else if (first.BaseData.ID == second.BaseData.ID)
                        {
                            return 0;
                        }
                        else
                        {
                            //return -1;
                            return editorOrderBy == EditorOrderBy.ASC ? -1 : 1;
                        }
                    });
            }
            else if (editorSortBy == EditorSortBy.Name)
            {
                items.Sort(
                    delegate (D first, D second)
                    {
                        if (first.BaseData.Name.CompareTo(second.BaseData.Name) > 0)
                        {
                            // return 1
                            return editorOrderBy == EditorOrderBy.ASC ? 1 : -1;
                        }
                        else if (first.BaseData.Name == second.BaseData.Name)
                        {
                            return 0;
                        }
                        else
                        {
                            //return -1;
                            return editorOrderBy == EditorOrderBy.ASC ? -1 : 1;
                        }
                    });
            }

            return items;
        }
    }
}
