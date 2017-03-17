using FPS.InventorySystem;
using ItemSystem;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public class WeaponItem : BaseItem
	{
        public WeaponItem(ISData _data, INSData _nsData) : base(_data, _nsData)
        {
            BaseData = _data;
            BaseNSData = _nsData;
            Init();
        }

        public WeaponItem() : base()
        {
            Init();
        }

        protected override void Init()
        {
            BaseData.Type = ItemTypeEnum.Weapon;
        }

        [SerializeField]
        private WeaponSData _baseData;
        public override ISData BaseData
        {
            get
            {
                if (_baseData == null)
                {
                    _baseData = new WeaponSData();
                }
                return _baseData;
            }
            set { _baseData = value as WeaponSData; }
        }

        [SerializeField]
        private WeaponNSData _baseNSData;
        public override INSData BaseNSData
        {
            get
            {
                if (_baseNSData == null)
                {
                    _baseNSData = new WeaponNSData();
                }
                return _baseNSData;
            }
            set { _baseNSData = value as WeaponNSData; }
        }

        private IInventory _inventory;
        public override IInventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        private IDBModel _dbModel;
        public override IDBModel DBModel
        {
            get
            {
                if (_dbModel == null)
                {
                    _dbModel = WeaponDBModel.LoadDb();
                }
                return _dbModel;
            }
            set { _dbModel = value; }
        }

        public override BaseItem FactoreNewItem(ISData data, INSData nsData)
        {
            WeaponItem newItem = new WeaponItem(data.SDeepClone() as ISData, nsData.NSDeepClone() as WeaponNSData);

            // In Case database default values get overriten, set them back here to default
            newItem.BaseData.UniqueUUID = System.Guid.NewGuid().ToString();
            newItem.BaseData.SlotID = -1;
            return newItem;
        }
    }
}