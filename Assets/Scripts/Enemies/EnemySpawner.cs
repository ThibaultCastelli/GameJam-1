using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTC;
using MusicTC;

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
        yield return new WaitForSeconds(currSpawnRate);
        MusicManager.Instance.IncreaseLayer(0);

        while (true)
        {
            IEnemy enemy = pool.Request().GetComponent<IEnemy>();
            enemy.Spawn();

            currSpawnRate = Mathf.Clamp(currSpawnRate - rateChanger, minSpawnRate, defaultSpawnRate);
            yield return new WaitForSeconds(currSpawnRate);
        }
    }

    public void StopSpawner()
    {
        List<GameObject> poolObjs = pool.GetAllActiveObject();
        foreach (GameObject obj in poolObjs)
        {
            IEnemy enemy = obj.GetComponent<IEnemy>();
            enemy.TakeDamage(57);
        }

        StopAllCoroutines();
    }
}
