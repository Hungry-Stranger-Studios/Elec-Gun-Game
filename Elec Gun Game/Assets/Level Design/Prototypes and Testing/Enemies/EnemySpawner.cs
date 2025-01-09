using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static int enemiesInMap;

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
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
         timeUntilSpawn -= Time.deltaTime;

        if (numRounds != 0 && enemiesInMap < numEnemiesPerRound){
            if(timeUntilSpawn <= 0){
                Instantiate(enemyPrefab,transform.position,Quaternion.identity);
                SetTimeUntilSpawn();
                enemiesInMap+= 1;
            }
        }
        //checks how many enemies are left and rounds left, then spawns that # of enemies
        
    }

    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minSpawnTime,maxSpawnTime);
    }
}
