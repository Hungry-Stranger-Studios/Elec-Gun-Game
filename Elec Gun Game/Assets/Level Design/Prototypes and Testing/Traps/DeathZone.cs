using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
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
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Death();
            Debug.Log("Death() called in EnemyController");
            
            //TODO: Add enemy controller script with a kill command.

        }
    }

    public void Disable()
    {
        zoneCollider.enabled = false;
    }

    public void Enable()
    {
        zoneCollider.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        //Drawing a collider gizmo
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }
}
