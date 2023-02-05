using System;
using System.Collections.Generic;
using UnityEngine;

namespace IgnSDK
{
    public class GameObjectPool : MonoBehaviour
    {
        // Private fields

        private readonly List<Pool> pools = new List<Pool>();
        private bool initialized = false;

        // GameObjectPool

        public T Take<T>() where T : class, IPoolable
        {
            if (initialized == false)
                Awake();

            var pool = GetPool(typeof(T));

            foreach (var poolObject in pool.pooledObjects)
            {
                if (poolObject is T poolable && !poolable.gameObject.activeSelf)
                {
                    poolable.gameObject.SetActive(true);
                    return poolable;
                }
            }

            var pooledObjSrc = GetSourceObject<T>();

            var newPooledObject = Instantiate(pooledObjSrc.gameObject, pooledObjSrc.gameObject.transform.parent).GetComponent<IPoolable>();
            newPooledObject.gameObject.SetActive(true);

            pool.pooledObjects.Add(newPooledObject);

            return newPooledObject as T;
        }

        public T Take<T>(Predicate<T> match) where T : class, IPoolable
        {
            if (initialized == false)
                Awake();

            var pool = GetPool(typeof(T));

            foreach (var poolObject in pool.pooledObjects)
            {
                var targetPoolObject = poolObject as T;

                if (poolObject is T poolable && poolable.gameObject.activeSelf == false && match(targetPoolObject))
                {
                    poolable.gameObject.SetActive(true);
                    return poolable;
                }
            }

            var pooledObjSrc = GetSourceObject(match);

            var newPooledObject = Instantiate(pooledObjSrc.gameObject, pooledObjSrc.gameObject.transform.parent).GetComponent<IPoolable>();
            newPooledObject.gameObject.SetActive(true);

            pool.pooledObjects.Add(newPooledObject);

            return newPooledObject as T;
        }

        public Pool GetPool(Type type)
        {
            for (var i = 0; i < pools.Count; i++)
            {
                if (pools[i].poolType.Equals(type))
                    return pools[i];
            }

            return null;
        }

        public void DisableAll()
        {
            if (initialized == false)
                Awake();

            for (var i = 0; i < pools.Count; i++)
            {
                foreach (var obj in pools[i].pooledObjects)
                {
                    obj.gameObject.SetActive(false);
                }
            }
        }

        private IPoolable GetSourceObject<T>() where T : class, IPoolable
        {
            var foundPool = GetPool(typeof(T));

            if (foundPool == null)
            {
                Debug.LogError($"Pool for {gameObject.name} not found");
                return null;
            }

            foreach (var prefab in foundPool.pooledObjects)
            {
                if (prefab is T)
                    return prefab;
            }

            Debug.LogError($"Missing pool prefab reference in {gameObject.name}");

            return null;
        }

        private IPoolable GetSourceObject<T>(Predicate<T> match) where T : class, IPoolable
        {
            var foundPool = GetPool(typeof(T));
            if (foundPool == null)
            {
                Debug.LogError($"Pool for {gameObject.name} not found");
                return null;
            }

            foreach (var prefab in foundPool.pooledObjects)
            {
                var targetPrefab = prefab as T;

                if (prefab is T && match(targetPrefab))
                    return prefab;
            }

            Debug.LogError($"Missing pool prefab reference in {gameObject.name}");

            return null;
        }

        private void FindAllPooledObjects()
        {
            pools.Clear();

            var poolables = transform.GetComponentsInChildren<IPoolable>(true);

            foreach (var poolable in poolables)
            {
                var type = poolable.GetType();

                var foundPool = GetPool(type);

                if (foundPool != null)
                {
                    foundPool.pooledObjects.Add(poolable);
                    poolable.gameObject.transform.SetParent(foundPool.parent);
                }
                else
                {
                    var nParent = gameObject;

                    var nPool = new Pool() { poolType = type, parent = nParent.transform };

                    pools.Add(nPool);
                    nPool.pooledObjects.Add(poolable);
                    poolable.gameObject.transform.SetParent(nParent.transform);
                }
            }
        }

        private void Awake()
        {
            if (initialized)
                return;

            FindAllPooledObjects();
            initialized = true;
        }
    }

    [Serializable]
    public class Pool
    {
        public Type poolType;
        public Transform parent;
        public readonly List<IPoolable> pooledObjects = new List<IPoolable>();
    }
}
