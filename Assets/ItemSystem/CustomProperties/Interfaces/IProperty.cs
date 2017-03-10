

namespace FPS.ItemSystem.CustomProperty
{
    public interface IProperty
    {
        string Type { get; set; }
        string Key { get; set; }
        string Value { get; set; }
        string Serialize(object obj);
        T Deserialize<T>();
    }
}