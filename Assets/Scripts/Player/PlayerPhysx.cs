using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysx : MonoBehaviour
{
    [HideInInspector]
    public Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy;
        IPowerUp powerUp;

        if (collision.TryGetComponent<IEnemy>(out enemy))
        {
            player.TakeDamage(enemy.Damage);
            enemy.TakeDamage(1);
        }

        else if (collision.TryGetComponent<IPowerUp>(out powerUp))
        {
            powerUp.PowerUp(player);
            collision.gameObject.SetActive(false);
        }
    }
}
