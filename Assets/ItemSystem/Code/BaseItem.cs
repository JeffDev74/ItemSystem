using System;
using UnityEngine;

namespace FPS.ItemSystem
{
    [System.Serializable]
    public abstract class BaseItem : ICoreData
    {
        public abstract SData BaseData { get; set; }

        public abstract NSData BaseNSData { get; set; }
    }
}