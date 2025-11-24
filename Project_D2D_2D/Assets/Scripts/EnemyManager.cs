using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    float currentTimeBetweenSpawns;

    Transform enemiesParent;

    public static EnemyManager Instance;

    private void Awake()
    {
       //to call it from other scripts
       if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //make sure the name 'Enemies' is the same as what we have in unity
        enemiesParent = GameObject.Find("Enemies").transform;
    }

    private void Update()
    {
        currentTimeBetweenSpawns -= Time.deltaTime;

        if(currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-16, 16), Random.Range(-8, 8));
    }

    void SpawnEnemy()
    {
        //below code create enemy in a random position and quaternion.identity makes it ignore rotaion 
        var e = Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);
        e.transform.SetParent(enemiesParent);

    }
    public void DestoryAllEnemies()
    {
        foreach(Transform e in enemiesParent)
        {
            Destroy(e.gameObject);
        }
    }
}
