﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;

    public float speed = 3f;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float distance = 10.0f;
    public HPManager healthbar;
    public GameObject gameOverScreen;
    private AudioSource audioSource;
    public AudioClip hitClip;


    float mx;
    float my;

    int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthbar.SetHealth(health);
        sr = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");


    }

    private void FixedUpdate()
    {
        if (health > 0)
        {
            if (mx < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            rb.velocity = new Vector2(mx, my) * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 10)
        {
            health -= 1;
            healthbar.SetHealth(health);

            audioSource.PlayOneShot(hitClip);

            if (health <= 0)
            {
                gameOverScreen.SetActive(true);
                rb.velocity = Vector2.zero;
            }
        }
    }
}
