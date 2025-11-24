using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    //when you put serializefield, it allows us to change the value from the editor
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] float moveSpeed = 6;

    Animator anim;
    Rigidbody2D rb;
    
    int maxHealth = 100;
    int currentHealth;

    bool dead = false;
    float moveHorizontal, moveVertical;
    Vector2 movement;
    int facingDirection = 1; //1 = right, -1 = left

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthText.text = maxHealth.ToString();
    }

    private void Update()
    {
        if (dead)
        {
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;

        anim.SetFloat("velocity", movement.magnitude);

        if(movement.x != 0)
        {
            facingDirection = movement.x > 0 ? 1 : -1;
        }

        transform.localScale = new Vector2(facingDirection, 1);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    //this detects when there is a collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if(enemy != null)
        {
            Hit(20);
        }
    }
    void Hit(int damage)
    {
        anim.SetTrigger("hit");
        currentHealth -= damage;
        //below code makes sure that current health doesn't go over the span of 0 to maxHealth
        healthText.text = Mathf.Clamp(currentHealth, 0, maxHealth).ToString();
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        GameManager.instance.GameOver();
    }
}
