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

        private void OnEnable()
        {
            dbModel = (SQLiteItemDBModel)target;
            dbModel.SetDbPath();
            dbExist = File.Exists(dbModel.DB_FULL_PATH);
        }

        public override void OnInspectorGUI()
        {
            //dbModel = (SQLiteItemDBModel)target;

            if(dbExist)
            {
                if (GUILayout.Button("Empty DB"))
                {
                    Debug.Log("erasing records.");
                }
            }
            else
            {
                if(GUILayout.Button("Create DB"))
                {
                    File.Create(dbModel.DB_FULL_PATH).Dispose();
                    AssetDatabase.Refresh();
                    dbModel.RunMigration();
                }
            }
            
            //base.OnInspectorGUI();
            Repaint();
        }
    }
}