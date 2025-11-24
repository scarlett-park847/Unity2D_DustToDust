using System;
using UnityEngine;

public class ArmoredEnemyHealth
{
    private readonly EnemyBase enemy;
    private readonly float damageReduction; // 0.3 = 30% less damage

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public ArmoredEnemyHealth(EnemyBase enemy, int maxHealth, float damageReduction)
    {
        this.enemy = enemy;
        this.damageReduction = Mathf.Clamp01(damageReduction);
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeHit(int damage)
    {
        int finalDamage = Mathf.CeilToInt(damage * (1f - damageReduction));
        CurrentHealth -= finalDamage;

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

