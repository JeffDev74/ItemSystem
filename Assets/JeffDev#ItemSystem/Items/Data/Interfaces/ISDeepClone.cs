using UnityEngine;
using FPS.ItemSystem;
namespace FPS
{
    public interface ISDeepClone<T> where T : class
    {
        T SDeepClone();
    }
}