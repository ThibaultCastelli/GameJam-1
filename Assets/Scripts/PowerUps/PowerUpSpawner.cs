using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTC;

public class PowerUpSpawner : MonoBehaviour
{
    private RandomPool pool;

    private void Awake()
    {
        pool = GetComponent<RandomPool>();
    }

    public void SpawnPowerUp(Vector2 pos)
    {
        IPowerUp powerUp = pool.Request().GetComponent<IPowerUp>();
        powerUp.Spawn(pos);
    }
}
