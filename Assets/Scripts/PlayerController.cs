using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;

    public float speed = 3f;
    Rigidbody2D rb;

    public float distance = 10.0f;
    public HPManager healthbar;


    float mx;
    float my;

    int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthbar.SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");


    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx, my) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 10)
        {
            health -= 1;
            healthbar.SetHealth(health);
        }
    }
}
