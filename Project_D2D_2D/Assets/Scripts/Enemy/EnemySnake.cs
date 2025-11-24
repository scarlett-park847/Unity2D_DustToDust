using UnityEngine;

public class EnemySnake : EnemyBase
{
    protected override int MaxHealth => 120;
    protected override float MoveSpeed => 2f;

    protected override IEnemyHealth CreateHealthStrategy()
    {
        return new SimpleEnemyHealth(this, MaxHealth);
    }

    protected override IEnemyMovement CreateMovementStrategy()
    {
        return new SimpleFollowMovement(this, MoveSpeed);
    }
    protected override IEnemyWeapon CreateWeaponStrategy()
    {
        return null;
    }
        
}
