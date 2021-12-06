using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTC;
using CustomVariablesTC;
using ObserverTC;
using SFXTC;

public class Player : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] SpriteRenderer shipSprite;
    [SerializeField] GameObject canonUp;
    [SerializeField] GameObject canonDown;

    [Header("INFOS")]
    [SerializeField] [Range(1f, 20f)] float speed = 5f;
    [SerializeField] [Range(0f, 2f)] float accelerationTime = 1f;

    [SerializeField] IntReference life;

    [Header("EVENTS")]
    [SerializeField] NotifierInt camShakeNotifier;
    [SerializeField] Notifier playerDeathNotifier;

    [Header("AUDIO")]
    [SerializeField] SFXEvent shootSFX;
    [SerializeField] SFXEvent shootUpSFX;

    private Rigidbody2D rb;

    private Pool bulletPool;
    private Shield shield;

    private Camera cam;
    private float camHeight;
    private float camWidth;

    private float spriteWidth;
    private float spriteHeight;

    private float horizontalInput;
    private float prevHorizontalInput = 0;
    private float verticalInput;
    private float prevVerticalInput = 0;
    private float currAcceleration = 0f;

    public Shield Shield => shield;
    public GameObject CanonUp => canonUp;
    public GameObject CanonDown => canonDown;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        bulletPool = GetComponentInChildren<Pool>();

        shield = GetComponentInChildren<Shield>();
        shield.gameObject.SetActive(false);

        cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        spriteWidth = shipSprite.sprite.bounds.size.x / 1.7f;
        spriteHeight = shipSprite.sprite.bounds.size.y / 1.7f;

        life.Value = 3;
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

        camShakeNotifier.Notify(3);

        life.Value -= amount;

        if (life.Value <= 0)
        {
            playerDeathNotifier.Notify();
            gameObject.SetActive(false);
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet newBullet = bulletPool.Request().GetComponent<Bullet>();
            newBullet.Spawn(rb.position);

            if (canonUp.activeInHierarchy)
            {
                Bullet newBulletUp = bulletPool.Request().GetComponent<Bullet>();
                newBulletUp.Spawn(canonUp.transform.position);
            }

            if (canonDown.activeInHierarchy)
            {
                Bullet newBulletDown = bulletPool.Request().GetComponent<Bullet>();
                newBulletDown.Spawn(canonDown.transform.position);

                shootUpSFX.Play();
            }
            else
            {
                shootSFX.Play();
            }
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
