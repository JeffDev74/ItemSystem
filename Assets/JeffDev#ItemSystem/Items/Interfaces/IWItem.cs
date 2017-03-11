using UnityEngine;

namespace FPS.ItemSystem
{
	public interface IWItem
	{
        BaseItem BaseItem { get; set; }
        Transform TheTransform { get; }
	}
}