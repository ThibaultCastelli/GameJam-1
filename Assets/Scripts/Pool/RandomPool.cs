using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolTC
{
    /// <summary>
    /// Class for creating a Random Pool.
    /// </summary>
    public class RandomPool : MonoBehaviour
    {
        #region Variables
        [Header("COMPONENTS")]
        [Tooltip("The objects that will be in the pools.")]
        [SerializeField] GameObject[] prefabs;
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

        List<List<GameObject>> pools = new List<List<GameObject>>();
        #endregion

        private void Awake()
        {
            // Instantiate and fill the pools
            for (int i = 0; i < prefabs.Length; i++)
            {
                pools.Add(new List<GameObject>(defaultPoolSize));
                Debug.Log(i);
            }

            for (int i = 0; i < defaultPoolSize; i++)
            {
                for (int j = 0; j < pools.Count; j++)
                {
                    GameObject currentPrefab = Instantiate(prefabs[j], poolContainer);
                    currentPrefab.SetActive(false);
                    pools[j].Add(currentPrefab);
                }
            }
        }

        /// <summary>
        /// Find and return the first inactive game object in the a random pool.
        /// If none is available, returns a null object, or a new game object added to the pool 
        /// depending on the type of pool it is.
        /// </summary>
        /// <returns>An inactive game object or a null game object.</returns>
        public GameObject Request()
        {
            int randomIndex = Random.Range(0, pools.Count);
            
            for (int i = 0; i < pools[randomIndex].Count; i++)
            {
                if (!pools[randomIndex][i].activeInHierarchy)
                    return pools[randomIndex][i];
            }

            if (fixedSize)
            {
                Debug.LogError("ERROR : The pool is full and has a fixed size.");
                return new GameObject("Null Object");
            }
            else if (pools[randomIndex].Count == maxPoolSize)
            {
                Debug.LogError("ERROR : You have reach the maximum size of the pool.");
                return new GameObject("Null Object");
            }
            else
            {
                GameObject newPrefab = Instantiate(prefabs[randomIndex], poolContainer);
                pools[randomIndex].Add(newPrefab);
                return newPrefab;
            }
        }

        /// <summary>
        /// Reset the pool to its default state.
        /// </summary>
        public void ResetPool()
        {
            foreach (List<GameObject> pool in pools)
            {
                foreach (GameObject obj in pool)
                    obj.SetActive(false);
            }
        }
    }
}
