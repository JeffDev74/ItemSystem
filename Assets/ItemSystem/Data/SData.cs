using FPS.ItemSystem.CustomProperty;

namespace FPS.ItemSystem
{
    [System.Serializable]
	public class SData
	{
        public int ID;
        public string ItemUUID;
        public string ItemName;
        public ItemTypeEnum ItemType;
        public PropertyManager Properties;
	}
}