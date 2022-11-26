using UnityEngine;

namespace ObjectPool
{
    public interface IViewServices
    {
        T Instantiate<T>(GameObject prefab, Transform root);
        void Destroy(GameObject value);
    }
}
