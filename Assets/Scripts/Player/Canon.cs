using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("INFOS")]
    [SerializeField] [Range(0.5f, 5f)] float duration = 2f;

    private void OnEnable()
    {
        StartCoroutine(LifeCycle());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
