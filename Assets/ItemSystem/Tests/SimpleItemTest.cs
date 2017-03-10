using System;
using FPS.ItemSystem;
using UnityEngine;

namespace FPS
{
    public class SimpleItemTest : MonoBehaviour, IItem
    {
        #region IItem Interface implementation

        private ResourceItem _baseItem;
        public BaseItem BaseItem
        {
            get { return _baseItem; }
            set { _baseItem = value as ResourceItem; }
        }

        private Transform _theTransform;
        public Transform TheTransform
        {
            get
            {
                if(_theTransform == null)
                {
                    _theTransform = transform;
                }
                return _theTransform;
            }
        }

        #endregion IItem Interface implementation
    }
}