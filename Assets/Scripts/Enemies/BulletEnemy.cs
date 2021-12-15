using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] Vector2 spawnOffset;
    [SerializeField] [Range(1f, 20f)] float speed = 5f;

    private Rigidbody2D rb;

    private Camera cam;
    private float camWidth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        camWidth = cam.orthographicSize * cam.aspect;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.left * speed * Time.deltaTime);
        
        // Despawn bullet when outside of cam's view
        if (rb.position.x < -camWidth * 1.5f)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;

        if (collision.TryGetComponent<Player>(out player))
        {
            gameObject.SetActive(false);
            player.TakeDamage(1);
        }
    }

    public void Spawn(Vector2 spawnPos)
    {
        transform.position = spawnPos + spawnOffset;
        gameObject.SetActive(true);
    }
}
