using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    string state = "idle";

    GameObject player;

    public float speed = 1f;
    public float attackCooldown = 1f;
    float currentACD;
    public int health = 10;

    public GameObject projectile;
    public GameObject enemyBile;
    public float projectileSpeed = 10f;
    public GameObject gameOverScreen;

    private AudioSource shootingSound;
    public AudioClip shootingClip;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        currentACD = attackCooldown;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "active")
        {
            // this type of alien stays at a certain range from the player and fires projectiles
            Vector3 delta = player.transform.position - transform.position;
            delta.z = 0;

            // fire projectiles in a sort of cone pattern
            if (currentACD > 0)
            {
                currentACD -= 1 * Time.deltaTime;
            }
            else if (projectile)
            {
                float fireAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

                for (int i = -3; i < 4; i++)
                {
                    //Vector2 fireDirection = new Vector2(delta.x, delta.y).normalized;
                    float radAngle = (fireAngle + (25 * i)) * Mathf.Deg2Rad;
                    Vector2 fireDirection = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle)).normalized;
                    GameObject projectileClone = Instantiate(projectile);
                    projectileClone.transform.position = transform.position;

                    projectileClone.transform.rotation = Quaternion.Euler(0, 0, radAngle * Mathf.Rad2Deg);
                    projectileClone.GetComponent<Rigidbody2D>().velocity = fireDirection * projectileSpeed;
                    
                }
                shootingSound = gameObject.AddComponent<AudioSource>();
                //shootSound.clip = shootClip;
                shootingSound.PlayOneShot(shootingClip);
                Destroy(GetComponent<AudioSource>(), shootingClip.length);

                currentACD = attackCooldown;
            }

            // flip the sprite when going left
            if (delta.x > 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
            delta = delta.normalized;

            transform.Translate(delta.x * speed * Time.deltaTime, delta.y * speed * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            health -= 1;
            if (health <= 0)
            {
                Destroy(gameObject);
                GameObject enemyBileClone = Instantiate(enemyBile, transform.position, transform.rotation);
                if (gameOverScreen)
                {
                    gameOverScreen.SetActive(true);
                }
            }
        }
        if (collision.gameObject.layer == 8)
        {
            // push alien out of hitbox
            Vector2 dir = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            dir = -dir.normalized;

            // essentially un-move the alien
            transform.Translate(new Vector3(dir.x * speed * Time.deltaTime, dir.y * speed * Time.deltaTime, 0));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // bump aliens out of walls when they get stuck
        if (collision.gameObject.layer == 8)
        {
            // push alien out of hitbox
            Vector2 dir = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            dir = -dir.normalized;

            // essentially un-move the alien
            transform.Translate(new Vector3(dir.x * speed * Time.deltaTime * 1.2f, dir.y * speed * Time.deltaTime * 1.2f, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyTrigger")
        {
            state = "active";
        }
    }
}
