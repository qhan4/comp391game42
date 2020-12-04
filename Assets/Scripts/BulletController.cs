using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform bullet;

    Rigidbody2D rb;

    public float life = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, life);
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * 400);

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy bullet on wall collision
        if (collision.gameObject.tag != "EnemyAttack")
        {
            Destroy(gameObject);
        }
    }
}
