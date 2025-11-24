using UnityEngine;

public class MeleeWeapon : IEnemyWeapon
{
    private readonly EnemyBase enemy;
    private float attackRange;
    private float attackCooldown;
    private int damage;

    private float cooldownTimer;

    public MeleeWeapon(EnemyBase enemy, float attackRange, float attackCooldown, int damage)
    {
        this.enemy = enemy;
        this.attackRange = attackRange;
        this.attackCooldown = attackCooldown;
        this.damage = damage;
        cooldownTimer = 0f;
    }

    public void Tick()
    {
        if (enemy.Target == null) return;

        // cooldown
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        float distance = Vector2.Distance(enemy.transform.position, enemy.Target.position);
        if (distance <= attackRange)
        {
            // trigger attack animation
            enemy.Anim?.SetTrigger("attack");

            // TODO: apply damage to player
            // example if you have a Player script with Hit():
            // var player = enemy.Target.GetComponent<Player>();
            // player?.Hit(damage);

            cooldownTimer = attackCooldown;
        }
    }
}
