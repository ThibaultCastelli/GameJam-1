using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverTC;

public class Tank : MonoBehaviour, IEnemy
{
    [Header("INFOS")]
    [SerializeField] [Range(1, 5)] int life = 2;
    [SerializeField] [Range(1, 3)] int damageOnCollision = 3;

    [SerializeField] [Range(2f, 10f)] float speed = 3f;

    [SerializeField] [Range(0, 100)] int dropRate = 80;

    [Header("EVENTS")]
    [SerializeField] NotifierVector2 dropPowerUpNotifier;
    [SerializeField] NotifierInt camShakeNotifier;

    private Rigidbody2D rb;

    private SpriteRenderer sprite;
    private float spriteHeight;

    private Camera cam;
    private float camWidth;

    private int currLife;

    public int Damage => damageOnCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();
        spriteHeight = sprite.sprite.bounds.size.y / 1.9f;

        cam = Camera.main;
        camWidth = cam.orthographicSize * cam.aspect * 1.2f;

        currLife = life;
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

        currLife = life;

        gameObject.SetActive(true);
    }

    public void TakeDamage(int amount)
    {
        currLife--;

        if (currLife <= 0)
        {
            bool drop = Random.Range(0, 100) < dropRate ? true : false;
            if (drop)
                dropPowerUpNotifier.Notify(transform.position);

            gameObject.SetActive(false);
        }
    }
}
