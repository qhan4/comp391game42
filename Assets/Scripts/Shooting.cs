using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float bulletSpeed = 10;
    private AudioSource shootSound;
    public AudioClip shootClip;
    

    Vector2 lookDirection;
    float lookAngle;

    public float fireRate;
    private float nextFire;

    void Update()
    {
        //lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
            //shootSound.PlayOneShot(shootClip);
            //shootSound.Stop();

            //Invoke(nameof(stopShootSound), shootClip.length);

           shootSound = gameObject.AddComponent<AudioSource>();
            //shootSound.clip = shootClip;
            shootSound.PlayOneShot(shootClip);
            Destroy(GetComponent<AudioSource>(), shootClip.length);

        }

    }

    private void stopShootSound()
    {
        shootSound.Stop();
    }




}