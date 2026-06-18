using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnInterval = 2f;
    public float spawnRadius = 15f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPoint =
            Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 spawnPosition =
            new Vector3(
                randomPoint.x,
                0.5f,
                randomPoint.y);

        Instantiate(enemyPrefab,
                    spawnPosition,
                    Quaternion.identity);
    }
}