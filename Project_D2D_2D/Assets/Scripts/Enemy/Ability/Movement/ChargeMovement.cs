using System;
using UnityEngine;

public class ChargeMovement : IEnemyMovement
{
    private EnemyBase enemy;
    public float MoveSpeed { get; private set; }

    private float distanceToCharge;
    private float chargeSpeed;
    private float prepareTime;

    private bool isCharging = false;
    private bool isPreparingCharge = false;
    private float prepareTimer = 0f;
    private float chargeTimer = 0f;


    public ChargeMovement(
        EnemyBase enemy,
        float moveSpeed,
        float distanceToCharge,
        float chargeSpeed,
        float prepareTime)
    {
        this.enemy = enemy;
        MoveSpeed = moveSpeed;
        this.distanceToCharge = distanceToCharge;
        this.chargeSpeed = chargeSpeed;
        this.prepareTime = prepareTime;
    }

    

    //public void Tick()
    //{
    //    if (!WaveManager.Instance.WaveRunning()) return;
    //    if (isPrepartingCharge) return;

    //    if (enemy.Target != null)
    //    {
    //        Vector3 direction = enemy.Target.position - enemy.transform.position;
    //        direction.Normalize();

    //        //this part of code will move the enemy towards the player all the time
    //        enemy.transform.position += direction * enemy.MoveSpeedValue * Time.deltaTime;

    //        //This part of code makes enemy face the direction epending on the player is on the left or right
    //        //it checks player's and enemy's x position
    //        //then we set x of local scale of the enemy either -1 or 1 depending on the player is on the left or right
    //        var playerToTheRight = enemy.Target.position.x > enemy.transform.position.x;
    //        enemy.transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);

    //        //below means the enemy is close enough to the player to prepare charging
    //        if (!isCharging && Vector2.Distance(enemy.transform.position, enemy.Target.position) < distanceToCharge)
    //        {
    //            isPrepartingCharge = true;
    //            Invoke("StartCharging", prepareTime);
    //        }
    //    }
    //}

    //void StartCharging()
    //{
    //    isPrepartingCharge = false;
    //    isCharging = true;
    //    enemy.MoveSpeedValue = chargeSpeed;
    //}
    public void Tick()
    {
        if (!WaveManager.Instance.WaveRunning()) return;

        // preparing delay
        if (isPreparingCharge)
        {
            prepareTimer -= Time.deltaTime;
            if (prepareTimer <= 0f)
            {
                isPreparingCharge = false;
                isCharging = true;
                chargeTimer = 0.2f; // charge duration example
            }
            return;
        }

        // charging movement
        if (isCharging)
        {
            DoChargeMovement();
            return;
        }

        // normal movement
        DoApproachMovement();

        // check distance for trigger
        float distance = Vector2.Distance(enemy.transform.position, enemy.Target.position);
        if (distance < distanceToCharge)
        {
            isPreparingCharge = true;
            prepareTimer = prepareTime;
        }
    }

    private void DoApproachMovement()
    {
        Vector3 dir = (enemy.Target.position - enemy.transform.position).normalized;
        enemy.transform.position += dir * MoveSpeed * Time.deltaTime;

        FlipSprite(dir);
    }

    private void DoChargeMovement()
    {
        chargeTimer -= Time.deltaTime;

        Vector3 dir = (enemy.Target.position - enemy.transform.position).normalized;
        enemy.transform.position += dir * chargeSpeed * Time.deltaTime;

        FlipSprite(dir);

        if (chargeTimer <= 0f)
            isCharging = false;
    }

    private void FlipSprite(Vector3 dir)
    {
        bool right = dir.x > 0;
        enemy.transform.localScale = new Vector2(right ? -1 : 1, 1);
    }
}

