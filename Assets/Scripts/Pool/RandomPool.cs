using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolTC
{
    /// <summary>
    /// Base class for creating a Pool pattern.
    /// </summary>
    public class RandomPool : MonoBehaviour
    {
        #region Variables
        [Header("COMPONENTS")]
        [Tooltip("The object that will be in the pool.")]
        [SerializeField] GameObject[] prefabs;
        [Tooltip("Object use to return null and prevent null exception.")]
        [SerializeField] GameObject nullObject;
        [Tooltip("The object where the pool will be created.")]
        [SerializeField] Transform poolContainer;
        [Space]

        [Header("POOL INFOS")]
        [Tooltip("If set to false : The pool can be resize dynamically if you request a game object while they are all in use in the " +
            "pool.\nIf set to true : The pool will not be resize dynamically, so if all game objects are in use in the pool and you " +
            "request a new one, nothing will happen.")]
        [SerializeField] bool fixedSize;
        [SerializeField] [Range(1, 1000)] int defaultPoolSize = 1;
        [SerializeField] [Range(1, 1000)] int maxPoolSize = 10;

        List<GameObject> pool = new List<GameObject>();
        #endregion

        private void Awake()
        {
            // Instantiate and fill the pool
            pool = new List<GameObject>(defaultPoolSize);

            for (int i = 0; i < defaultPoolSize; i++)
            {
                int randomPrefabIndex = Random.Range(0, prefabs.Length);
                GameObject currentPrefab = Instantiate(prefabs[randomPrefabIndex], poolContainer);
                currentPrefab.SetActive(false);
                pool.Add(currentPrefab);
            }
        }

        /// <summary>
        /// Find and return the first inactive game object in the pool.
        /// If none is available, returns a null object, or a new game object added to the pool 
        /// depending on the type of pool it is.
        /// </summary>
        /// <returns>An inactive game object or a null game object.</returns>
        public GameObject Request()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                    return pool[i];
            }

            if (fixedSize)
            {
                Debug.LogError("ERROR : The pool is full and has a fixed size.");
                return nullObject;
            }
            else if (pool.Count == maxPoolSize)
            {
                Debug.LogError("ERROR : You have reach the maximum size of the pool.");
                return nullObject;
            }
            else
            {
                int randomPrefabIndex = Random.Range(0, prefabs.Length);
                GameObject newPrefab = Instantiate(prefabs[randomPrefabIndex], poolContainer);
                pool.Add(newPrefab);
                return newPrefab;
            }
        }
    }
}
