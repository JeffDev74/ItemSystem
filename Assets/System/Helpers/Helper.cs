using FPS.ItemSystem;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FPS
{
    public class Helper : MonoBehaviour
    {
        public static D FactoreData<D>(string data) where D : SData
        {
            try
            {
                Stream stream = DecodeItem(data);
                BinaryFormatter formatter = new BinaryFormatter();
                D newData = formatter.Deserialize(stream) as D;
                return newData;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError(e.StackTrace);
                //throw new Exception("Failed to factore item data.");
                return null;
            }
        }

        public static Stream DecodeItem(string data)
        {
            try
            {
                byte[] byteArray = System.Convert.FromBase64String(data);
                var stream = new MemoryStream(byteArray);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                Debug.LogError(ex.StackTrace);
                throw new System.Exception("Failed to decode item data.");
            }
        }
    }
}