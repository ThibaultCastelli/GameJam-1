using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTC;
using CustomVariablesTC;
using ObserverTC;
using SFXTC;
using MusicTC;
using UnityEngine.SceneManagement;
using EasingTC;

public class Player : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] SpriteRenderer shipSprite;
    [SerializeField] GameObject canonUp;
    [SerializeField] GameObject canonDown;
    [SerializeField] GameObject shield;

    [Header("INFOS")]
    [SerializeField] [Range(1f, 20f)] float speed = 5f;
    [SerializeField] [Range(0f, 2f)] float accelerationTime = 1f;
    [SerializeField] [Range(0f, 2f)] float timeOfInvicibilityAfterDamage = 0.5f;

    [SerializeField] IntReference life;

    [SerializeField] [Range(0f, 5f)] float timeToDie = 1f;

    [Header("EVENTS")]
    [SerializeField] NotifierInt camShakeNotifier;
    [SerializeField] Notifier playerDeathNotifier;

    [Header("AUDIO")]
    [SerializeField] SFXEvent shootSFX;
    [SerializeField] SFXEvent shootUpSFX;
    [SerializeField] SFXEvent damageSFX;
    [SerializeField] SFXEvent deathSFX;

    [Header("ANIM")]
    [SerializeField] EasingColor flashAnim;
    [SerializeField] Animator animator;

    private Rigidbody2D rb;

    private Pool bulletPool;

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

    private bool canShoot = true;
    private bool canTakeDamage = true;

    public GameObject Shield => shield;
    public GameObject CanonUp => canonUp;
    public GameObject CanonDown => canonDown;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        bulletPool = GetComponentInChildren<Pool>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy;
        IPowerUp powerUp;

        if (collision.TryGetComponent<IEnemy>(out enemy))
        {
            TakeDamage(enemy.Damage);
            enemy.TakeDamage(1);
        }

        else if (collision.TryGetComponent<IPowerUp>(out powerUp))
        {
            powerUp.PowerUp(this);
            collision.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int amount)
    {
        if (shield.gameObject.activeInHierarchy || !canTakeDamage)
            return;

        camShakeNotifier.Notify(3);

        life.Value -= amount;

        if (life.Value <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            damageSFX.Play();

            if (MusicManager.Instance.CurrentLayer == 1 || MusicManager.Instance.CurrentLayer == 3)
                MusicManager.Instance.IncreaseLayer();
            MusicManager.Instance.IncreaseLayer();

            canTakeDamage = false;
            canShoot = false;
            flashAnim.PlayAnimationInOut();
            StartCoroutine(InvicibleAfterTakingDamage());
        }
    }

    private IEnumerator InvicibleAfterTakingDamage()
    {
        yield return new WaitForSeconds(timeOfInvicibilityAfterDamage);
        canTakeDamage = true;
        canShoot = true;
        flashAnim.StopAnimation();
    }

    private IEnumerator Die()
    {
        animator.SetTrigger("Death");
        MusicManager.Instance.Stop(0);
        deathSFX.Play();
        playerDeathNotifier.Notify();

        speed = 0;
        canShoot = false;

        yield return new WaitForSeconds(timeToDie);
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
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
