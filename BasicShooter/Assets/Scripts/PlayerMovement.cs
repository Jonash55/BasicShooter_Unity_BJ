using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float healthPoints = 100f;
    internal bool isDead = false;
    public int score = 0;
    public int enemiesLeft;

    public Text enemiesCount;

    public Rigidbody2D rb;
    public Animator animator;
    public Camera cam;

    public GameManager gameManager;

    Vector2 movement;
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesCount.text = "Enemies left " + enemiesLeft.ToString();
        if (!isDead)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            animator.SetFloat("Speed", movement.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            FindObjectOfType<GameManager>().EndGame();
        }  
        if (enemiesLeft == 0)
        {
            FindObjectOfType<GameManager>().GameWon();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BulletPistolE"))
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

        if (collision.gameObject.CompareTag("BulletMachineGunE"))
        {
            healthPoints -= 15f;

            if (healthPoints <= 0)
            {
                animator.SetBool("Death", true);
                animator.SetBool("Shotgun", false);
                isDead = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("BulletShotgunE"))
        {
            healthPoints -= 50f;

            if (healthPoints <= 0)
            {
                animator.SetBool("Death", true);
                animator.SetBool("Shotgun", true);
                isDead = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            Destroy(collision.gameObject);
        }
    }
}
