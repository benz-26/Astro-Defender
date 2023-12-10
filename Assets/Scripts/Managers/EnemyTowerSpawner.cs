using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Array of spawn positions
    public GameObject enemyPrefab;
    public float spawnRate = 6f; // Set the spawn rate to 6 seconds

    private float nextSpawnTime;

    private void Start()
    {
        // Initialize the next spawn time
        SetNextSpawnTime();
    }

    private void OnEnable()
    {
        // Reset the spawn time whenever the script is enabled
        SetNextSpawnTime();
    }

    private void Update()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            SetNextSpawnTime();
        }
    }

    private void SpawnEnemy()
    {
        // Randomly select a spawn point from the array
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn an enemy at the selected spawn point
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + spawnRate;
    }
}



