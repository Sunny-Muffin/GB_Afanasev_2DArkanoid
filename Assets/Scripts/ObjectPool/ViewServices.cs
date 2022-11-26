using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    internal sealed class ViewServices : IViewServices
    {
        private readonly Dictionary<string, ObjectPool> _viewCache = new Dictionary<string, ObjectPool>(12);

        public T Instantiate<T>(GameObject prefab, Transform root)
        {
            if (!_viewCache.TryGetValue(prefab.name, out ObjectPool viewPool))
            {
                viewPool = new ObjectPool(prefab, root);
                _viewCache[prefab.name] = viewPool;
            }

            if (viewPool.Pop().TryGetComponent(out T component))
            {
                return component;
            }

            throw new InvalidOperationException($"{typeof(T)} not found");
        }

        public void Destroy(GameObject value)
        {
            _viewCache[value.name].Push(value);
        }
    }
}
