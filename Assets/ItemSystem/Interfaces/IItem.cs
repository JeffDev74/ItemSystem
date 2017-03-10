using UnityEngine;

namespace FPS.ItemSystem
{
	public interface IItem
	{
        BaseItem BaseItem { get; set; }
        Transform TheTransform { get; }
	}
}