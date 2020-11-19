using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;

    public float speed = 3f;
    Rigidbody2D rb;

    public float distance = 10.0f;


    float mx;
    float my;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector2.zero;
    }
}
