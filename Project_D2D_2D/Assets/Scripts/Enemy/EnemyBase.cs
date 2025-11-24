using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : MonoBehaviour
{
    // Force child classes to supply these values
    protected abstract int MaxHealth { get; }
    protected abstract float MoveSpeed { get; }
    protected abstract IEnemyMovement CreateMovementStrategy();


    public Animator Anim;
    public Transform Target; // player
    public IEnemyMovement movement;
    public int currentHealth;
    public float MoveSpeedValue;


    // Use Awake/Start as virtual so children can extend them
    protected virtual void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        // Validate max health
        if (MaxHealth <= 0)
        {
            Debug.LogError($"{name} was spawned with INVALID maxHealth ({MaxHealth}). " +
                           "Destroying object to prevent logic errors.", this);
            Destroy(gameObject);
            return;   // prevent the rest of Start from running
        }


        Target = GameObject.Find("Player").transform;
        if (Target == null)
        {
            Debug.LogError($"Target not found for enemy {name}. " +
                           "Destroying object to prevent logic errors.", this);
            Destroy(gameObject);
            return;
        }

        currentHealth = MaxHealth;
        MoveSpeedValue = MoveSpeed;

        movement = CreateMovementStrategy();
    }

    //protected virtual void:
    // 1. The method is accessible to the class and its child classes
    // 2. The method can be overridden in a child class
    // 3. The child class may replace or extend the behavior
    protected virtual void Update()
    {
        if (movement != null)
        {
            movement.Tick();
        }
        else
        {
            DefaultMove();
        }
    }

    // Base movement: walk straight toward player
    // protected void:
    // 1. The method is accessible to this class and its child classes
    // 2. The method cannot be overridden by child classes
    // 3. The method’s behavior is fixed — inheritance cannot change it
    protected void DefaultMove()
    {
        if (!WaveManager.Instance.WaveRunning()) return;

        if (Target == null) return;

        Vector3 direction = Target.position - transform.position;
        direction.Normalize();

        transform.position += direction * MoveSpeed * Time.deltaTime;

        // Flip sprite based on player position
        bool playerToTheRight = Target.position.x > transform.position.x;
        transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);
    }

    public virtual void Hit(int damage)
    {
        currentHealth -= damage;

        if (Anim != null)
        {
            Anim.SetTrigger("hit");
        }
        Debug.LogError($"{name} got hit!", this);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
