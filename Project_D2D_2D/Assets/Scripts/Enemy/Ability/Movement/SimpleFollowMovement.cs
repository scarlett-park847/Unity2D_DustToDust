using System;
using UnityEngine;

public class SimpleFollowMovement : IEnemyMovement
{
    private readonly EnemyBase enemy;
    public float MoveSpeed { get; private set; }

    public SimpleFollowMovement(EnemyBase enemy, float moveSpeed)
    {
        this.enemy = enemy;
        MoveSpeed = moveSpeed;
    }

    public void Tick()
    {
        // Stop moving if wave isn't active
        if (!WaveManager.Instance.WaveRunning()) return;
        if (enemy.Target == null) return;

        // Move toward target
        Vector3 direction = (enemy.Target.position - enemy.transform.position).normalized;
        enemy.transform.position += direction * MoveSpeed * Time.deltaTime;

        // Flip sprite
        bool playerToTheRight = enemy.Target.position.x > enemy.transform.position.x;
        enemy.transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);
    }
}

