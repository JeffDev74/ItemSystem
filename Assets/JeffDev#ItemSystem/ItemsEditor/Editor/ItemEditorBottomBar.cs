using UnityEngine;

namespace ItemSystem
{
    public partial class ItemEditor
    {
        void BottomBar()
        {
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));

            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
            GUILayout.Label("Status Bar");

            if (tmpItem != null)
            {
                //GUILayout.Label("Item");
                GUI.contentColor = Color.cyan;
                GUILayout.Label(" [" + tmpItem.BaseData.Name + "] ");
                GUI.contentColor = defaultFontColor;
                //GUILayout.Label("is selected.");
            }

            GUILayout.Label("Listing [" + showingItemsCount + "] out of [" + totalItemsCount + "] items.");
            GUILayout.EndHorizontal();

            GUILayout.EndHorizontal();
        }
    }
}