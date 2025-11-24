using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : MonoBehaviour
{
    // Force child classes to provide base stats
    protected abstract int MaxHealth { get; }
    protected abstract float MoveSpeed { get; }

    // Factories for strategies
    protected abstract IEnemyMovement CreateMovementStrategy();
    protected abstract IEnemyHealth CreateHealthStrategy();
    protected abstract IEnemyWeapon CreateWeaponStrategy();

    public Animator Anim;
    public Transform Target; // player
    public IEnemyMovement movement;
    public IEnemyHealth health;
    public IEnemyWeapon weapon;


    // Use Awake/Start as virtual so children can extend them
    protected virtual void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        // Find player
        var player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError($"Player not found for enemy {name}. Destroying.", this);
            Destroy(gameObject);
            return;
        }

        Target = player.transform;

        // Create strategies
        health = CreateHealthStrategy();
        if (health == null || health.MaxHealth <= 0)
        {
            Debug.LogError($"{name} has invalid health strategy or MaxHealth. Destroying.", this);
            Destroy(gameObject);
            return;
        }

        movement = CreateMovementStrategy();
        if (movement == null)
        {
            Debug.LogError($"{name} has invalid move strategy. Destroying.", this);
            Destroy(gameObject);
            return;
        }

        weapon = CreateWeaponStrategy();
    }

    //protected virtual void:
    // 1. The method is accessible to the class and its child classes
    // 2. The method can be overridden in a child class
    // 3. The child class may replace or extend the behavior
    // protected void:
    // 1. The method is accessible to this class and its child classes
    // 2. The method cannot be overridden by child classes
    // 3. The method’s behavior is fixed — inheritance cannot change it

    protected virtual void Update()
    {
        movement?.Tick();
        weapon?.Tick();
    }

    
    public virtual void Hit(int damage)
    {
        health?.TakeHit(damage);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
