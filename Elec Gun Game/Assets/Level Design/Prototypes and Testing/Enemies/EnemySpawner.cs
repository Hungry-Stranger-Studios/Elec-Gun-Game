using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static int enemiesInMap;
    static bool isSpawning = false;
    static int currentRound = 1;

    [SerializeField]
    private int numEnemiesPerRound;

    [SerializeField]
    private float timeBetweenRounds;

    [SerializeField]
    private int numRounds;

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;

    private float timeUntilSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        isSpawning=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRound <= numRounds && isSpawning == false && enemiesInMap == 0){
            StartCoroutine(Spawning());
        }
        
        //checks how many enemies are left and rounds left, then spawns that # of enemies
        
    }
    private IEnumerator Spawning()
    {
        Debug.Log($"Round: {currentRound}");
        isSpawning = true;

        while (enemiesInMap < numEnemiesPerRound)
        {
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);

            yield return new WaitForSeconds(spawnDelay); // Wait for the delay before spawning

            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemiesInMap++;
            Debug.Log($"Enemy spawned. Total enemies in map: {enemiesInMap}");
        }

        while (enemiesInMap > 0)
        {
            yield return null; 
        }

        if (currentRound <= numRounds)
        {
            Debug.Log($"Round {currentRound} completed. Waiting for the next round...");
            yield return new WaitForSeconds(timeBetweenRounds);
            currentRound++;
            numEnemiesPerRound += 3; // Optionally increase enemies per round
            Debug.Log($"Preparing for Round {currentRound}");
        }

        isSpawning = false;
    }

    
    public static void DecrementEnemyCount()
    {
        if (enemiesInMap > 0)
        {
            enemiesInMap--;
            Debug.Log($"Enemy removed. Enemies in map: {enemiesInMap}");
        }
        
    }
}