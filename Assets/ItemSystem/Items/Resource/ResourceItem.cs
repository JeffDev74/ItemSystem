using System;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public class ResourceItem : BaseItem
    {
        public ResourceItem()
        {
            BaseData.ItemType = ItemTypeEnum.Resource;
        }

        [SerializeField]
        private ResourceSData _baseData;
        public override SData BaseData
        {
            get
            {
                if(_baseData == null)
                {
                    _baseData = new ResourceSData();
                }
                return _baseData;
            }
            set { _baseData = value as ResourceSData; }
        }

        [SerializeField]
        private ResourceNSData _baseNSData;
        public override NSData BaseNSData
        {
            get
            {
                if(_baseNSData == null)
                {
                    _baseNSData = new ResourceNSData();
                }
                return _baseNSData;
            }
            set { _baseNSData = value as ResourceNSData; }
        }
    }
}