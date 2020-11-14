using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform bullet;
    public float speed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * 400);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
