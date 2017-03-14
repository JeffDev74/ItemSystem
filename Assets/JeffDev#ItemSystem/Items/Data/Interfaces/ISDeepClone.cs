using UnityEngine;
using FPS.ItemSystem;
namespace FPS.ItemSystem
{
    public interface ISDeepClone<T> where T : class
    {
        T SDeepClone();
    }
}