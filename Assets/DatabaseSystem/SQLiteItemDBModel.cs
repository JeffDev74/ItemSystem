using UnityEngine;
using System.Collections.Generic;

// Sqlite
using Mono.Data.Sqlite;
using System.Data;
using FPS.ItemSystem;
using FPS.ItemSystem.CustomProperty;

namespace FPS
{
    public class SQLiteItemDBModel : MonoBehaviour
    {
        public static SQLiteItemDBModel Instance;

        public string DB_PATH = "";
        public string DB_NAME = "";

        void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            DB_PATH = "StreamingAssets/Databases/";
            DB_NAME = "Items.bytes";
        }

        public string GetDBPath()
        {
            return "URI=file:" + Application.dataPath + "/StreamingAssets/Databases/Items.bytes";
        }

        public void CreateItem(ICoreData ItemData)
        {
            string _strDBName = GetDBPath();
            IDbConnection _connection = new SqliteConnection(_strDBName);
            IDbCommand _command = _connection.CreateCommand();
            string tableName = "items";
            string sql = "";

            _connection.Open();

            if (ItemData != null)
            {
                if (ItemData.BaseData is ISerializeData)
                {
                    ISerializeData itemSerializeDataInterface = ItemData.BaseData as ISerializeData;
                    if (itemSerializeDataInterface == null)
                    {
                        Debug.LogError("The external DB item data does not implement the interface ISerializeData");
                        return;
                    }

                    IPropertySerializer propertiesInterface = ItemData.BaseData.Properties as IPropertySerializer;
                    string serializedProperties = string.Empty;
                    if (propertiesInterface != null)
                    {
                        serializedProperties = propertiesInterface.Serialize();
                    }

                    sql = string.Format("INSERT INTO " + tableName + " (item_uuid, type, data, properties)" +
                    " VALUES ( \"{0}\", \"{1}\", \"{2}\", \"{3}\");",
                    ItemData.BaseData.UniqueUUID,
                    ItemData.BaseData.Type,
                    itemSerializeDataInterface.SerializeItemData(),
                    serializedProperties
                    );
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                }
                else
                {
                    Debug.LogError("External DB item [" + ItemData.BaseData.Name + "] does not implement ISerializeData interface.");
                }

            }
            else
            {
                Debug.Log("The external DB item is null.");
            }

            _command.Dispose();
            _command = null;

            _connection.Close();
            _connection = null;
        }

        public BaseItem[] GetAllItems(string player_uuid = "")
        {
            List<BaseItem> items = new List<BaseItem>();

            string conn = GetDBPath();

            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);

            dbconn.Open();

            #region DB Structure
            //CREATE TABLE `items` (
            // `id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            // `object_id`	TEXT DEFAULT '00000000-0000-0000-0000-000000000000',
            // `owner_id`	INTEGER NOT NULL,
            // `object_owner_id`	TEXT,
            // `type`	TEXT NOT NULL,
            // `data`	BLOB NOT NULL,
            // `position`	BLOB NOT NULL,
            // `rotation`	BLOB NOT NULL
            //);
            #endregion DB Structure

            IDbCommand dbcmd = dbconn.CreateCommand();
            string sql = "";
            if (string.IsNullOrEmpty(player_uuid))
            {
                sql = "SELECT id, item_uuid, type, data, properties " + "FROM items;";
            }
            else
            {
                sql = "SELECT id, item_uuid, type, data, properties " + "FROM items WHERE owner_uuid=\"" + player_uuid + "\";";
            }

            dbcmd.CommandText = sql;

            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                //int id = reader.GetInt32(0);
                //string item_uuid = reader.GetString(1);
                //string type = reader.GetString(2);
                string data = reader.GetString(3);
                string propertiesData = reader.GetString(4);


                var newData = Helper.FactoreData<SData>(data);

                BaseItem extItemDB = null;
                if (newData != null)
                {
                    ISerializeData iSerializeInterface = newData as ISerializeData;

                    if (iSerializeInterface != null)
                    {
                        extItemDB = iSerializeInterface.FactoryCloneItemFromData();
                    }
                    else
                    {
                        Debug.Log("The external DB item data does not implement the ISerializable interface");
                    }

                    IPropertySerializer propertySerializerInterface = extItemDB.BaseData.Properties as IPropertySerializer;
                    if (propertySerializerInterface != null)
                    {
                        propertySerializerInterface.Deserialize<List<Property>>(propertiesData);
                    }
                    else
                    {
                        Debug.Log("The external DB item data property does not implement the interface IPropertySerializer");
                    }
                }

                if (items.Contains(extItemDB) == false && extItemDB != null)
                {
                    items.Add(extItemDB);
                }
                else
                {
                    Debug.LogError("Trying to add a duplicated external DB item skipping.");
                }
            }

            reader.Close();
            reader = null;

            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;

            return items.ToArray();
        }

        public void UpdateItem(string itemUUID, ICoreData itemDBData)
        {
            string sql = "";
            string tableName = "items";
            string conn = GetDBPath();
            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();

            ISerializeData itemSerializeDataInterface = null;

            if (itemDBData.BaseData is ISerializeData)
            {
                itemSerializeDataInterface = itemDBData.BaseData as ISerializeData;

                if (itemSerializeDataInterface == null)
                {
                    Debug.LogError("The external DB item data does not implement the interface ISerializeData");
                    return;
                }
            }


            #region DB Structure
            //CREATE TABLE `items` (
            // `id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            // `object_id`	TEXT DEFAULT '00000000-0000-0000-0000-000000000000',
            // `owner_id`	INTEGER NOT NULL,
            // `object_owner_id`	TEXT,
            // `type`	TEXT NOT NULL,
            // `data`	BLOB NOT NULL,
            // `position`	BLOB NOT NULL,
            // `rotation`	BLOB NOT NULL,
            // `timeout` INTEGER
            //);
            #endregion DB Structure

            // should i add player uuid on the where statment????
            sql = string.Format("UPDATE \"" + tableName + "\" SET data=\"{0}\" WHERE item_uuid=\"{2}\";",
            itemSerializeDataInterface.SerializeItemData(), itemUUID);

            dbcmd.CommandText = sql;
            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;
        }

        public void UpdateItem(string itemUUID, ICoreData[] itemDBData)
        {
            for (int i = 0; i < itemDBData.Length; i++)
            {
                UpdateItem(itemUUID, itemDBData[i]);
            }
        }

        public void DeleteItem(int externalDB_id)
        {
            string sql = "";
            string tableName = "items";
            string conn = GetDBPath();
            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            sql = "DELETE FROM " + tableName + " WHERE item_uuid = " + externalDB_id + ";";

            dbcmd.CommandText = sql;
            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;
        }

        public void DeleteItem(string itemUUID)
        {
            string sql = "";
            string tableName = "items";
            string conn = GetDBPath();
            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            sql = "DELETE FROM " + tableName + " WHERE item_uuid = \"" + itemUUID + "\";";

            dbcmd.CommandText = sql;
            dbcmd.ExecuteNonQuery();


            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;
        }
    }
}