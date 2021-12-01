using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 1f)] float smallShakeDuration = 0.5f;
    [SerializeField] [Range(0.1f, 1f)] float smallShakeStrengh = 0.5f;

    [SerializeField] [Range(0.1f, 1f)] float mediumShakeDuration = 0.5f;
    [SerializeField] [Range(0.1f, 1f)] float mediumShakeStrengh = 0.5f;

    [SerializeField] [Range(0.1f, 1f)] float strongShakeDuration = 0.5f;
    [SerializeField] [Range(0.1f, 1f)] float strongShakeStrengh = 0.5f;

    Vector3 defaultPos;

    private void Awake()
    {
        defaultPos = transform.position;
    }

    public void Shake(int index)
    {
        StopAllCoroutines();
        transform.position = defaultPos;

        switch (index)
        {
            case 1:
                StartCoroutine(ShakeCoroutine(smallShakeDuration, smallShakeStrengh));
                break;
            case 2:
                StartCoroutine(ShakeCoroutine(mediumShakeDuration, mediumShakeStrengh));
                break;
            case 3:
                StartCoroutine(ShakeCoroutine(strongShakeDuration, strongShakeStrengh));
                break;
        }
        
    }

    private IEnumerator ShakeCoroutine(float duration, float strengh)
    {
        float t = duration;

        while (t != 0)
        {
            float randomX = Random.Range(-strengh, strengh);
            float randomY = Random.Range(-strengh, strengh);
            transform.position = new Vector3(randomX, randomY, defaultPos.z);

            t = Mathf.Clamp01(t - Time.deltaTime);
            yield return null;
        }

        transform.position = defaultPos;
    }
}
