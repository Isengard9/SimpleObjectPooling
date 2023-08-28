using System;
using System.Collections.Generic;
using UnityEngine;

namespace WFIG
{
    public class SimpleObjectPool : MonoBehaviour
    {
        private static SimpleObjectPool instance;
        public static SimpleObjectPool Instance=> instance;
        public ObjectPoolList poolItems;

        private Dictionary<string, Queue<GameObject>> objectPools = new Dictionary<string, Queue<GameObject>>();

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("There is another simple object pool script in scene");
                Destroy(instance);
            }

            instance = this;
            
            
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            InitializePools();
        }

        private void InitializePools()
        {
            foreach (var poolItem in poolItems.ItemList)
            {
                if(poolItem is null) continue;
                var prefab = poolItem.prefab;
                int poolSize = poolItem.poolSize;

                if (!objectPools.ContainsKey(poolItem.objectName))
                {
                    objectPools[poolItem.objectName] = new Queue<GameObject>();
                }

                for (int i = 0; i < poolSize; i++)
                {
                    var obj = Instantiate(prefab, transform);
                    obj.SetActive(false);
                    objectPools[poolItem.objectName].Enqueue(obj);
                }
            }
        }

        public GameObject GetObjectFromPool(string poolObjectName)
        {
            if (objectPools.ContainsKey(poolObjectName) && objectPools[poolObjectName].Count > 0)
            {
                GameObject obj = objectPools[poolObjectName].Dequeue();
             
                obj.SetActive(true);
                return obj;
            }
            else
            {
                Debug.LogError("Object pool is empty or the requested prefab is not in the pool.");
                return null;
            }
        }

        public void ReturnObjectToPool(string objectName, GameObject obj)
        {
            obj.SetActive(false);
            objectPools[objectName].Enqueue(obj);
        }
    }
}