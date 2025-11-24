using UnityEngine;

public class Gun : MonoBehaviour
{
    //[Header("Config")] : This is editor-only — it has no effect in code execution. Just adds a visual label above serialized fields in the Inspector for organization.
    [Header("Prefabs")]
    [SerializeField] GameObject muzzle;
    [SerializeField] Transform muzzlePosition;
    [SerializeField] GameObject projectile;

    [Header("Config")]
    [SerializeField] float fireDistance = 10;
    [SerializeField] float fireRate = 0.5f;

    Transform player;
    Vector2 offset;

    private float timeSinceLastShot = 0f;
    Transform closestEnemy;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timeSinceLastShot = fireRate;
        player = GameObject.Find("Player").transform;

    }

    private void Update()
    {
        transform.position = (Vector2)player.position + offset;
        FindClosestEnemy();
        AimAtEnemy();
        Shooting();
    }

    void FindClosestEnemy()
    {
        closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach(Enemy enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if(distance < closestDistance && distance <= fireDistance)
            {
                //below closest distance logic makes sure that the gun points to the closest enemy all the time
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

    }

    void AimAtEnemy()
    {
        if(closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = (Vector2)player.position + offset;
        }
    }

    void Shooting()
    {
        if (closestEnemy == null)
            return;

        timeSinceLastShot += Time.deltaTime;
        if(timeSinceLastShot >= fireRate)
        {
            Shoot();
            timeSinceLastShot = 0;
        }
    }

    void Shoot()
    {
        anim.SetTrigger("shoot");

        var muzzleGo = Instantiate(muzzle, muzzlePosition.position, transform.rotation);
        muzzleGo.transform.SetParent(transform);
        Destroy(muzzleGo, 0.05f);

        var projectTileGo = Instantiate(projectile, muzzlePosition.position, transform.rotation);
        //Destory after 3 sec even if we miss the enemy
        Destroy(projectTileGo, 3);
    }

    public void SetOffSet(Vector2 o)
    {
        offset = o;
    }
}
