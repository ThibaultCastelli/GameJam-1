using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverTC;

public class Obstacle : MonoBehaviour, IEnemy
{
    [SerializeField] [Range(1f, 10f)] float minSpeed = 1f;
    [SerializeField] [Range(1f, 10f)] float maxSpeed = 10f;

    [SerializeField] [Range(0.1f, 2f)] float minScale = 0.5f;
    [SerializeField] [Range(0.1f, 2f)] float maxScale = 1.4f;

    [SerializeField] [Range(10f, 50f)] float minRotationSpeed = 15f;
    [SerializeField] [Range(10f, 50f)] float maxRotationSpeed = 30f;

    [SerializeField] [Range(1, 3)] int damageOnCollision = 1;

    [SerializeField] [Range(0, 100)] int dropRate = 30;

    [SerializeField] NotifierVector2 dropPowerUpNotifier;

    private Rigidbody2D rb;

    private SpriteRenderer sprite;
    float spriteHeight;

    private Camera cam;
    private float camWidth;

    private float speed = 1f;
    private float scale = 1f;
    private float rotationSpeed = 0f;

    public int Damage => damageOnCollision;

    private void Awake()
    {
        GetRandomScale();

        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);

        sprite = GetComponent<SpriteRenderer>();
        spriteHeight = sprite.sprite.bounds.size.y / 2;

        cam = Camera.main;
        camWidth = cam.orthographicSize * cam.aspect * 1.2f;

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
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

        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        float yPos = Random.Range(-cam.orthographicSize + spriteHeight, cam.orthographicSize - spriteHeight);
        transform.position = new Vector2(camWidth, yPos);
        GetRandomScale();
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        gameObject.SetActive(true);
    }

    private void GetRandomScale()
    {
        scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale);
    }
}
