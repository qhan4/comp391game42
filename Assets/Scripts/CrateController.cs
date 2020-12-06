using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    public Sprite brokenCrate;

    SpriteRenderer sr;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            // break when the player shoots it
            sr.sprite = brokenCrate;
            rb.simulated = false;
        }
    }
}
