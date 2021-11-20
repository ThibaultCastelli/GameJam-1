using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTC;
using CustomVariablesTC;

public class Player : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float speed = 5f;
    [SerializeField] [Range(0f, 2f)] float accelerationTime = 1f;

    [SerializeField] IntReference life;

    private Rigidbody2D rb;

    private Pool bulletPool;
    private Shield shield;

    private Camera cam;
    private float camHeight;
    private float camWidth;

    private SpriteRenderer sprite;
    private float spriteWidth;
    private float spriteHeight;

    private float horizontalInput;
    private float prevHorizontalInput = 0;
    private float verticalInput;
    private float prevVerticalInput = 0;
    private float currAcceleration = 0f;

    public Shield Shield => shield;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        bulletPool = GetComponentInChildren<Pool>();

        shield = GetComponentInChildren<Shield>();
        shield.gameObject.SetActive(false);

        cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        sprite = GetComponentInChildren<SpriteRenderer>();
        spriteWidth = sprite.sprite.bounds.size.x / 2;
        spriteHeight = sprite.sprite.bounds.size.y / 2;
    }

    private void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void TakeDamage(int amount)
    {
        if (shield.gameObject.activeInHierarchy)
            return;

        life.Value -= amount;

        // TODO - Handle player's damage
        Debug.Log("Player's life : " + life.Value);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet newBullet = bulletPool.Request().GetComponent<Bullet>();
            newBullet.Spawn(rb.position);
        }
    }

    private void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 nextPos;

        // Acceleration
        if (horizontalInput != 0 || verticalInput != 0)
        {
            currAcceleration = Mathf.Clamp01(currAcceleration + Time.deltaTime / accelerationTime);
            nextPos = new Vector2(horizontalInput, verticalInput).normalized * speed * Time.deltaTime * currAcceleration;

            prevHorizontalInput = horizontalInput;
            prevVerticalInput = verticalInput;
        }

        // Deceleration
        else
        {
            currAcceleration = Mathf.Clamp01(currAcceleration - Time.deltaTime / accelerationTime);
            nextPos = new Vector2(prevHorizontalInput, prevVerticalInput).normalized * speed * Time.deltaTime * currAcceleration;
        }

        nextPos += rb.position;
        nextPos = new Vector2(Mathf.Clamp(nextPos.x, -camWidth + spriteWidth, camWidth - spriteWidth),
            Mathf.Clamp(nextPos.y, -camHeight + spriteHeight, camHeight - spriteHeight));
        rb.MovePosition(nextPos);
    }
}
