using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTC;
using ObserverTC;

public class Ranger : MonoBehaviour, IEnemy
{
    [SerializeField] [Range(5f, 20f)] float speed = 15f;
    [SerializeField] [Range(0.1f, 2f)] float fireRate = 1f;
    [SerializeField] [Range(1, 3)] int damageOnCollision = 1;

    [SerializeField] [Range(0, 100)] int dropRate = 50;

    [SerializeField] NotifierVector2 dropPowerUpNotifier;

    private Pool bulletPool;

    private Rigidbody2D rb;

    private SpriteRenderer sprite;
    private float spriteHeight;

    private Camera cam;
    private float camWidth;

    private IEnumerator shootCoroutine;

    public int Damage => damageOnCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        bulletPool = GetComponentInChildren<Pool>();

        sprite = GetComponent<SpriteRenderer>();
        spriteHeight = sprite.sprite.bounds.size.y / 2;

        cam = Camera.main;
        camWidth = cam.orthographicSize * cam.aspect * 1.2f;

        shootCoroutine = Shoot();
    }

    private void Update()
    {
        if (transform.position.x < -camWidth)
            TakeDamage(1);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.MovePosition(rb.position + Vector2.left * speed * Time.deltaTime);
    }

    public void Spawn()
    {
        float yPos = Random.Range(-cam.orthographicSize + spriteHeight, cam.orthographicSize - spriteHeight);
        transform.position = new Vector2(camWidth, yPos);

        gameObject.SetActive(true);
        StartCoroutine(shootCoroutine);
    }

    public void TakeDamage(int amount)
    {
        bool drop = Random.Range(0, 100) < dropRate ? true : false;

        if (drop)
            dropPowerUpNotifier.Notify(transform.position);

        StopCoroutine(shootCoroutine);
        bulletPool.ResetPool();
        gameObject.SetActive(false);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            BulletEnemy newBullet = bulletPool.Request().GetComponent<BulletEnemy>();
            newBullet.Spawn(rb.position);
        }
    }
}
