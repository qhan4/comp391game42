using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    string state = "idle";

    GameObject player;

    public float speed = 2f;
    public int enemyType = 1; // 1 = the melee aliens, 2 = the ranged aliens
    public float attackCooldown = 1f;
    float currentACD;

    public GameObject enemyBile;
    public float projectileSpeed = 10f;

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
            if (enemyType == 2)
            {
                // this type of alien stays at a certain range from the player and fires projectiles
                Vector3 delta = player.transform.position - transform.position;
                delta.z = 0;

                // fire projectiles in a sort of cone pattern
                if (currentACD > 0)
                {
                    currentACD -= 1 * Time.deltaTime;
                }
                else if (enemyBile)
                {
                    float fireAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

                    for (int i = -1; i < 2; i++)
                    {
                        //Vector2 fireDirection = new Vector2(delta.x, delta.y).normalized;
                        float radAngle = (fireAngle + (30 * i)) * Mathf.Deg2Rad;
                        Vector2 fireDirection = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle)).normalized;
                        GameObject projectileClone = Instantiate(enemyBile);
                        projectileClone.transform.position = transform.position;

                        projectileClone.transform.rotation = Quaternion.Euler(0, 0, fireAngle);
                        projectileClone.GetComponent<Rigidbody2D>().velocity = fireDirection * projectileSpeed;
                    }

                    currentACD = attackCooldown;
                }

                // flip the sprite when going left
                if (delta.x < 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }

                if (delta.magnitude < 8)
                {
                    // if they are too close, the aliens attempts to move away
                    delta = -delta;
                }
                else if (delta.magnitude < 9)
                {
                    delta = Vector3.zero;
                }
                delta = delta.normalized;

                transform.Translate(delta.x * speed * Time.deltaTime, delta.y * speed * Time.deltaTime, 0);
            }
            else
            {
                // the basic alien type moves towards the player

                if (currentACD > 0)
                {
                    currentACD -= 1 * Time.deltaTime;
                }
                else
                {
                    // get vector from alien to player
                    Vector3 delta = player.transform.position - transform.position;
                    delta.z = 0;
                    delta = delta.normalized;

                    // flip the sprite when going left
                    if (delta.x < 0)
                    {
                        sr.flipX = true;
                    }
                    else
                    {
                        sr.flipX = false;
                    }

                    transform.Translate(delta.x * speed * Time.deltaTime, delta.y * speed * Time.deltaTime, 0);

                    // the currentACD gets reset in OnCollisionEnter2D when the alien hits a player
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
            GameObject enemyBileClone = Instantiate(enemyBile, transform.position, transform.rotation);
        }
        if (collision.gameObject.layer == 8)
        {
            // push alien out of hitbox
            Vector2 dir = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            dir = -dir.normalized;

            // essentially un-move the alien
            transform.Translate(new Vector3(dir.x * speed * Time.deltaTime, dir.y * speed * Time.deltaTime, 0));
        }
        if (collision.gameObject.tag == "Player")
        {
            // put attack on cooldown (to give the player time to distance themselves)
            currentACD = attackCooldown;
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
