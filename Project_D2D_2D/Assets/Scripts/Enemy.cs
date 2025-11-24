using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float speed = 2f;

    private int currentHealth;

    Animator anim;
    Transform target; //follow target

    private void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();


    }
    private void Update()
    {
        if(target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            //this part of code will move the enemy towards the player all the time
            transform.position += direction * speed * Time.deltaTime;

            //This part of code makes enemy face the direction epending on the player is on the left or right
            //it checks player's and enemy's x position
            //then we set x of local scale of the enemy either -1 or 1 depending on the player is on the left or right
            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);
        }
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        //below trigger name should match with the trigger that we set in the engine
        //it is case sensitive
        anim.SetTrigger("hit");

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
