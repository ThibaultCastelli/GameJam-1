using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 5f)] float duration = 2f;

    private void OnEnable()
    {
        StartCoroutine(LifeCycle());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy;

        if (collision.TryGetComponent<IEnemy>(out enemy))
        {
            enemy.TakeDamage(3);
        }
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
