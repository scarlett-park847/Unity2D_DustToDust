using UnityEngine;

public class EnemyCharger : EnemyBase
{
    protected override int MaxHealth => 200;
    protected override float MoveSpeed => 2f;

    [Header("Charging")]
    [SerializeField] float distanceToCharge = 5f;
    [SerializeField] float ChargeSpeed = 12f;
    [SerializeField] float prepareTime = 2f;

    protected override IEnemyHealth CreateHealthStrategy()
    {
        return new SimpleEnemyHealth(this, MaxHealth);
    }

    protected override IEnemyMovement CreateMovementStrategy()
    {
        return new ChargeMovement(
            this,
            MoveSpeed,
            distanceToCharge,
            ChargeSpeed,
            prepareTime
        );
    }
    protected override IEnemyWeapon CreateWeaponStrategy()
    {
        return null;
    }

}
