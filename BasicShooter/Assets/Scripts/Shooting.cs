using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool isDead = false;
    public bool pistol = true;
    public bool m4 = false;
    public bool shotgun = false;
    float delay;
    private float shotgunDelay = 1f;
    private float m4Delay = 0.1f;
    public float bulletForce = 20f;
    public AudioSource sourceM4;
    public AudioSource sourceShotgun;
    public AudioSource sourcePistol;

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.tag = "BulletPistolP";
        sourceM4 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isDead = GameObject.Find("Player").GetComponent<PlayerMovement>().isDead;
        if (pistol)
        {
            if (Input.GetButtonDown("Fire1") && !isDead)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                sourcePistol.Play();
            }
        }
        else if (m4)
        {
            if (Time.time > delay)
            {
                delay = Time.time + m4Delay;
                if (Input.GetButton("Fire1") && !isDead)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                    sourceM4.Play();
                }
            }          
        }
        else if (shotgun)
        {
            if (Time.time > delay)
            {
                delay = Time.time + shotgunDelay;
                if (Input.GetButton("Fire1") && !isDead)
                {
                    Vector3 bulletVec = new Vector3(firePoint.position.x + 0.25f, firePoint.position.y, firePoint.position.z);
                    Vector3 bulletVec2 = new Vector3(firePoint.position.x, firePoint.position.y + 0.25f, firePoint.position.z);
                    Vector3 bulletVec3 = new Vector3(firePoint.position.x, firePoint.position.y - 0.25f, firePoint.position.z);
                    Vector3 bulletVec4 = new Vector3(firePoint.position.x - 0.25f, firePoint.position.y, firePoint.position.z);

                    GameObject bullet = Instantiate(bulletPrefab, bulletVec, firePoint.rotation);
                    GameObject bullet2 = Instantiate(bulletPrefab, bulletVec2, firePoint.rotation);
                    GameObject bullet3 = Instantiate(bulletPrefab, bulletVec3, firePoint.rotation);
                    GameObject bullet4 = Instantiate(bulletPrefab, bulletVec4, firePoint.rotation);

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                    Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();

                    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                    rb2.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                    rb3.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                    rb4.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                    sourceShotgun.Play();
                }
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("m4"))
        {
            pistol = false;
            m4 = true;
            shotgun = false;
            bulletPrefab.tag = "BulletMachineGunP";
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("shotgun"))
        {
            pistol = false;
            m4 = false;
            shotgun = true;
            bulletPrefab.tag = "BulletShotgunP";
            Destroy(collision.gameObject);
        }
    }
}
