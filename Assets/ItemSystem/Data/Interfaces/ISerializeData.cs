
namespace FPS.ItemSystem
{
    public interface ISerializeData
    {
        string SerializeItemData();
        BaseItem FactoryCloneItemFromData();
    }
}
