using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[]  foodPrefabs;
    public GameObject healthBarPrefab;
    Collider2D spawnCollider;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float minSpeed;
    public float maxSpeed;
    private float spawnTimer;

    // private bool canSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnCollider = GetComponent<Collider2D>();
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer();
    }

    void SpawnTimer()
    {
        spawnTimer -= Time.deltaTime;
        if (!(spawnTimer <= 0)) return;
        SpawnFood();
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnFood()
    {
        GameObject food = Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)]);
        food.transform.position = GetRandomPosition();
        food.GetComponent<Food>().InitializeFood(Random.Range(minSpeed, maxSpeed), healthBarPrefab);
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x);
        float y = Random.Range(spawnCollider.bounds.min.y, spawnCollider.bounds.max.y);
        return new Vector3(x, y);
    }
}
