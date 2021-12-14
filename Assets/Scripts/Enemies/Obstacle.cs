using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverTC;
using SFXTC;

public class Obstacle : MonoBehaviour, IEnemy
{
    [Header("COMPONENTS")]
    [SerializeField] Sprite[] sprites;

    [Header("INFOS")]
    [SerializeField] [Range(1f, 10f)] float minSpeed = 1f;
    [SerializeField] [Range(1f, 10f)] float maxSpeed = 10f;

    [SerializeField] [Range(0.1f, 2f)] float minScale = 0.5f;
    [SerializeField] [Range(0.1f, 2f)] float maxScale = 1.4f;

    [SerializeField] [Range(10f, 50f)] float minRotationSpeed = 15f;
    [SerializeField] [Range(10f, 50f)] float maxRotationSpeed = 30f;

    [SerializeField] [Range(1, 3)] int damageOnCollision = 1;

    [SerializeField] [Range(0, 100)] int dropRate = 30;
    [SerializeField] [Range(0, 1000)] int scoreOnDeath = 100;

    [Header("EVENTS")]
    [SerializeField] NotifierVector2 dropPowerUpNotifier;
    [SerializeField] NotifierInt enemyDeathNotifier;

    [Header("AUDIO")]
    [SerializeField] SFXEvent explosionSFX;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;
    float spriteHeight;

    private Camera cam;
    private float camWidth;

    private float speed = 1f;
    private float scale = 1f;
    private float rotationSpeed = 0f;

    public int Damage => damageOnCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteHeight = spriteRenderer.sprite.bounds.size.y / 1.9f;

        cam = Camera.main;
        camWidth = cam.orthographicSize * cam.aspect * 1.2f;

        GetRandomSprite();
        GetRandomScale();
        GetRandomRotation();
    }

    private void Update()
    {
        if (transform.position.x < -camWidth)
            gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.MovePosition(rb.position + Vector2.left * speed * Time.deltaTime);
        rb.MoveRotation(rb.rotation + rotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        bool drop = Random.Range(0, 100) < dropRate ? true : false;
        if (drop)
            dropPowerUpNotifier.Notify(transform.position);

        explosionSFX.Play();

        enemyDeathNotifier.Notify(scoreOnDeath);
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        float yPos = Random.Range(-cam.orthographicSize + spriteHeight, cam.orthographicSize - spriteHeight);
        transform.position = new Vector2(camWidth, yPos);
        GetRandomScale();
        GetRandomRotation();
        GetRandomSprite();

        gameObject.SetActive(true);
    }

    private void GetRandomScale()
    {
        scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale);
    }

    private void GetRandomRotation()
    {
        int dir = Random.Range(0, 2) == 0 ? -1 : 1;
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed) * dir;
    }

    private void GetRandomSprite()
    {
        if (sprites.Length == 0)
            return;

        int randomIndex = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[randomIndex];
    }
}
