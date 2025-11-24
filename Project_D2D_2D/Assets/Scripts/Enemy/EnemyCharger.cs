using UnityEngine;

public class EnemyCharger : EnemyBase
{
    protected override int MaxHealth => 200;

    protected override float MoveSpeed => 2f;

    [Header("Charging")]
    [SerializeField] float distanceToCharge = 5f;
    [SerializeField] float ChargeSpeed = 12f;
    [SerializeField] float prepareTime = 2f;

    protected override IEnemyMovement CreateMovementStrategy()
    {
        return new ChargeMovement(
            this,
            distanceToCharge,
            ChargeSpeed,
            prepareTime
        );
    }
}
