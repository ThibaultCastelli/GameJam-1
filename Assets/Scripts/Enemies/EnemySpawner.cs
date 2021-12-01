using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTC;

public class EnemySpawner : MonoBehaviour
{
    [Header("INFOS")]
    [SerializeField] [Range(1f, 5f)] float defaultSpawnRate = 1f;
    [SerializeField] [Range(0.1f, 5f)] float minSpawnRate = 0.5f;
    [SerializeField] [Range(0.01f, 1f)] float rateChanger = 0.1f;
    
    private RandomPool pool;

    private float currSpawnRate;

    private void Awake()
    {
        pool = GetComponent<RandomPool>();
        currSpawnRate = defaultSpawnRate;
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(currSpawnRate);

            IEnemy enemy = pool.Request().GetComponent<IEnemy>();
            enemy.Spawn();

            currSpawnRate = Mathf.Clamp(currSpawnRate - rateChanger, minSpawnRate, defaultSpawnRate);
        }
    }
}
