using UnityEngine;

namespace ItemSystem
{
    public interface INSDeepClone<T> where T : class
    {
        T NSDeepClone();
    }
}