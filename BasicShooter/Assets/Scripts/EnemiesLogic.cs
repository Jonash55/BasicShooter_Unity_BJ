using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLogic : MonoBehaviour
{
    public float speed = 2f;
    public float stop = 2f;
    public float healthPoints = 100f;
    public float bulletForce = 20f;
    float delay;
    public float fireDelay = 3f;
    public float playerHp = 100f;
    private bool isDead = false;
    public float radius = 10f;

    public Transform target;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabEnemy;
    public Rigidbody2D rb;
    public Animator animator;
    private PolygonCollider2D enemyCollider;
    private SpriteRenderer enemySprite;

    Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyCollider = GetComponent<PolygonCollider2D>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos.x = target.position.x;
        playerPos.y = target.position.y;
        
        playerHp = GameObject.Find("Player").GetComponent<PlayerMovement>().healthPoints;
        
        if (!isDead)
        {         
            if (Vector2.Distance(transform.position, target.position) > stop && Vector2.Distance(transform.position, target.position) <= radius)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                animator.SetFloat("Speed", 1);
            }
            if (Vector2.Distance(transform.position, target.position) < stop && playerHp >= 1)
            {
                Shoot();
                animator.SetFloat("Speed", 0);
            }
      
        }
        if (isDead)
        {
            gameObject.tag = "Dead";
            enemyCollider.enabled = false;
            enemySprite.sortingOrder = -1;
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Vector2 lookDir = playerPos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }            
    }

    private void Shoot()
    {
        if (Time.time > delay)
        {
            delay = Time.time + fireDelay;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BulletPistolP"))
        {
            healthPoints -= 35f;

            if (healthPoints <= 0)
            {
                isDead = true;
                animator.SetBool("Death", true);
                animator.SetBool("Shotgun", false);                
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("BulletMachineGunP"))
        {
            healthPoints -= 15f;

            if (healthPoints <= 0)
            {
                isDead = true;
                animator.SetBool("Death", true);
                animator.SetBool("Shotgun", false);          
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }          
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("BulletShotgunP"))
        {
            healthPoints -= 25f;

            if (healthPoints <= 0)
            {
                isDead = true;
                animator.SetBool("Death", true);
                animator.SetBool("Shotgun", true);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            Destroy(collision.gameObject);
        }      
    }
}
