using System;
public class SimpleEnemyHealth : IEnemyHealth
{
    private readonly EnemyBase enemy;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public SimpleEnemyHealth(EnemyBase enemy, int maxHealth)
    {
        this.enemy = enemy;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeHit(int damage)
    {
        CurrentHealth -= damage;

        if (enemy.Anim != null)
        {
            enemy.Anim.SetTrigger("hit");
        }


        if (CurrentHealth <= 0)
        {
            enemy.Die();
        }
    }
}

