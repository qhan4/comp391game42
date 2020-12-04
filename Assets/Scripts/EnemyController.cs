using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    string state = "idle";

    GameObject player;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "active")
        {
            // get vector from alien to player
            Vector3 delta = player.transform.position - transform.position;
            delta = delta.normalized;

            transform.Translate(delta.x * speed * Time.deltaTime, delta.y * speed * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
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
