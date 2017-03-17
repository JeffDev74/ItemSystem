using UnityEngine;

namespace FPS
{
	public interface IStackableData
	{
        bool IsStackable { get; set; }

        bool DestroyOnUse { get; set; }

        int Quantity { get; set; }

        int StackableMax { get; set; }
    }
}