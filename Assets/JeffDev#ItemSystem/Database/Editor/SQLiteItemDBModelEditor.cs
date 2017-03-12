using System.IO;
using UnityEditor;
using UnityEngine;

namespace FPS
{
    [CustomEditor(typeof(SQLiteItemDBModel))]
    public class SQLiteItemDBModelEditor : Editor
    {
        SQLiteItemDBModel dbModel;
        bool dbExist = false;

        private void CheckDB()
        {
            dbModel = (SQLiteItemDBModel)target;
            dbModel.SetDbPath();
            dbExist = File.Exists(dbModel.DB_FULL_PATH);
        }

        private void OnEnable()
        {
            CheckDB();
        }

        public override void OnInspectorGUI()
        {
            if(dbExist)
            {
                if (GUILayout.Button("Empty DB"))
                {
                    if(EditorUtility.DisplayDialog("Empty Database", "Are you sure. Empty database?", "OK"))
                    {
                        dbModel.EmptyDatabase();
                    }
                }

                if (GUILayout.Button("Delete DB"))
                {
                    if (EditorUtility.DisplayDialog("Delete Database", "Are you sure. Delete database?", "OK"))
                    {
                        File.Delete(dbModel.DB_FULL_PATH + ".meta");
                        File.Delete(dbModel.DB_FULL_PATH);
                        CheckDB();
                        // To use this we need to pass the relative path to the 
                        // file instead of the full path
                        // AssetDatabase.DeleteAsset(dbModel.DB_FULL_PATH);

                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }
            }
            else
            {
                if(GUILayout.Button("Create DB"))
                {
                    File.Create(dbModel.DB_FULL_PATH).Dispose();
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    dbModel.RunMigration();
                }
            }
            
            //base.OnInspectorGUI();
            Repaint();
        }
    }
}