using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour, IPowerUp
{
    [SerializeField] [Range(0.5f, 5f)] float timeToDespawn = 2f;

    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    public void PowerUp(Player player)
    {
        player.Shield.gameObject.SetActive(false);
        player.Shield.gameObject.SetActive(true);
    }

    public void Spawn(Vector2 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeToDespawn);
        gameObject.SetActive(false);
    }
}
