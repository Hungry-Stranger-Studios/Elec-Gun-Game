using System.Collections;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject enemyPrefab; // Enemy to spawn
    [SerializeField] private Transform spawnLocation; // Location to spawn enemies
    [SerializeField] private int enemiesToSpawnPerActivation = 5; // Number of enemies to spawn per activation
    [SerializeField] private float spawnInterval = 1f; // Time between each enemy spawn
    [SerializeField] private SpawnerButton linkedButton; // Button that triggers the spawner

    private bool isSpawning = false;

    private void Awake()
    {
        // Automatically find a spawn location child object if not set
        if (spawnLocation == null)
        {
            Transform childSpawnPoint = transform.Find("SpawnPoint");
            if (childSpawnPoint != null)
            {
                spawnLocation = childSpawnPoint;
            }
            else
            {
                Debug.LogError("Spawn location not set and no 'SpawnPoint' child found. Please assign a spawn point.");
            }
        }

        // Automatically link button events if a button exists
        if (linkedButton != null)
        {
            linkedButton.OnButtonActivation += ActivateSpawner;
        }
        else
        {
            Debug.LogError("No linked button found for SpawnerController.");
        }
    }

    private void ActivateSpawner()
    {
        if (!isSpawning)
        {
            Debug.Log("Spawner activated by button.");
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;
        int spawnedEnemies = 0;

        while (spawnedEnemies < enemiesToSpawnPerActivation)
        {
            Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
            spawnedEnemies++;
            Debug.Log($"Enemy spawned by spawner. Total spawned: {spawnedEnemies}");
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
        Debug.Log("Spawner finished spawning enemies.");
    }
}
