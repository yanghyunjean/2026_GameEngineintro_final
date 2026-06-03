using UnityEngine;
using System.Collections;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 6f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);

            yield return new WaitForSeconds(waitTime);

            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Transform point =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemyPrefab, point.position, Quaternion.identity);
    }
}