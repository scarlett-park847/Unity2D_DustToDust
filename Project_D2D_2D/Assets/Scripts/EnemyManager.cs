using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[SerializeField] GameObject enemyPrefab;
    //[SerializeField] GameObject chargerPrefab;
    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] float timeBetweenSpawns = 0.5f;
    private float currentTimeBetweenSpawns;

    private Transform enemiesParent;

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
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("EnemyManager has no enemyPrefabs assigned.");
            return;
        }

        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];


        //below code create enemy in a random position and quaternion.identity makes it ignore rotaion 
        var enemy = Instantiate(prefabToSpawn, RandomPosition(), Quaternion.identity);
        if (enemiesParent != null)
        {
            enemy.transform.SetParent(enemiesParent);
        }

    }
    public void DestoryAllEnemies()
    {
        if (enemiesParent == null) return;

        foreach (Transform e in enemiesParent)
        {
            Destroy(e.gameObject);
        }
    }
}
