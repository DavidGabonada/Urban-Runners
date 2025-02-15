using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;  // Array to store multiple enemy prefabs
    public Transform spawnPoint;  // Specific spawn location
    public float spawnTime = 5f;  // Time interval between spawns

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            // Pick a random enemy from the array
            int randomIndex = Random.Range(0, enemies.Length);
            GameObject enemyToSpawn = enemies[randomIndex];

            // Instantiate the selected enemy
            Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}
