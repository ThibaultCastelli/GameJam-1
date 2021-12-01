using ObserverTC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("INFOS")]
    [SerializeField] [Range(0.5f, 5f)] float duration = 2f;

    [Header("EVENTS")]
    [SerializeField] NotifierInt camShakeNotifier;

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
            camShakeNotifier.Notify(2);
        }
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
