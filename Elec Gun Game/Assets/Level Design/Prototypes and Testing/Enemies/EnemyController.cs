using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Collider2D zoneCollider;

    private void Awake()
    {
        zoneCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().KillPlayer();
        }
    }
    public void Death(){
        Destroy(gameObject);
        EnemySpawner.DecrementEnemyCount();
        Debug.Log("Enemy Killed");

    }
}
