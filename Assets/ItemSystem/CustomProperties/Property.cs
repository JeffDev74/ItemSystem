using UnityEngine;

namespace FPS.ItemSystem.CustomProperty
{
    [System.Serializable]
    public class Property : IProperty
    {
        [SerializeField]
        public string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [SerializeField]
        public string _key;
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        [SerializeField]
        public string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Property(string key, object value)
        {
            this.Type = value.GetType().ToString();
            this.Key = key;
            this.Value = value.ToString();
        }

        //public abstract string Serialize(object obj);
        public virtual string Serialize(object obj) { return obj.ToString(); }

        //public abstract T Deserialize<T>();
        public virtual T Deserialize<T>() { return default(T); }
    }
}