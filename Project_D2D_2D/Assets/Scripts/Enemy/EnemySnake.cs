using UnityEngine;

public class EnemySnake : EnemyBase
{
    protected override int MaxHealth => 120;
    protected override float MoveSpeed => 2f;

    protected override IEnemyMovement CreateMovementStrategy()
    {
        // No custom movement → fallback to base DefaultMove()
        return null;
    }
}
