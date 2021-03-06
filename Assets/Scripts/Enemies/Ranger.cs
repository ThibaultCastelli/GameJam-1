using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTC;
using ObserverTC;
using SFXTC;

public class Ranger : MonoBehaviour, IEnemy
{
    [Header("INFOS")]
    [SerializeField] [Range(5f, 20f)] float speed = 15f;
    [SerializeField] [Range(0.1f, 2f)] float fireRate = 1f;

    [SerializeField] [Range(1, 3)] int damageOnCollision = 1;

    [SerializeField] [Range(0, 100)] int dropRate = 50;
    [SerializeField] [Range(0, 1000)] int scoreOnDeath = 300;

    [Header("EVENTS")]
    [SerializeField] NotifierVector2 dropPowerUpNotifier;
    [SerializeField] NotifierInt enemyDeathNotifier;

    [Header("AUDIO")]
    [SerializeField] SFXEvent shootSFX;

    private Pool bulletPool;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private Animator animator;

    private SpriteRenderer sprite;
    private float spriteHeight;

    private Camera cam;
    private float camWidth;

    private IEnumerator shootCoroutine;

    private float yEndPos;
    private float xEndPos;

    public int Damage => damageOnCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();

        bulletPool = GetComponentInChildren<Pool>();

        sprite = GetComponent<SpriteRenderer>();
        spriteHeight = sprite.sprite.bounds.size.y / 1.9f;

        cam = Camera.main;
        camWidth = cam.orthographicSize * cam.aspect;

        xEndPos = -camWidth;

        shootCoroutine = Shoot();
    }

    private void Update()
    {
        if (transform.position.x <= -camWidth)
            Despawn();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector2 dir = Vector2.MoveTowards(transform.position, new Vector2(xEndPos, yEndPos), Time.deltaTime * speed);
        rb.MovePosition(dir);
    }

    public void Spawn()
    {
        float yPos = Random.Range(-cam.orthographicSize + spriteHeight, cam.orthographicSize - spriteHeight);
        yEndPos = Random.Range(-cam.orthographicSize + spriteHeight, cam.orthographicSize - spriteHeight);

        transform.position = new Vector2(camWidth, yPos);

        gameObject.SetActive(true);
        boxCollider.enabled = true;
        StartCoroutine(shootCoroutine);
    }

    private IEnumerator Death()
    {
        animator.SetTrigger("Death");
        enemyDeathNotifier.Notify(scoreOnDeath);
        StopCoroutine(shootCoroutine);
        boxCollider.enabled = false;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        Despawn();
    }

    private void Despawn()
    {
        bulletPool.ResetPool();
        gameObject.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        bool drop = Random.Range(0, 100) < dropRate ? true : false;
        if (drop)
            dropPowerUpNotifier.Notify(transform.position);

        StartCoroutine(Death());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            BulletEnemy newBullet = bulletPool.Request().GetComponent<BulletEnemy>();
            newBullet.Spawn(rb.position);

            shootSFX.Play();
        }
    }
}
