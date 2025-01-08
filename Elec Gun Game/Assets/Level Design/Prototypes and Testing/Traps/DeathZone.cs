using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DeathZone : MonoBehaviour
{
    private Object player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the object being killed is a player
        if (collision.gameObject.Equals(player)) {
            player.GetComponent<PlayerMovement>().KillPlayer();
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            //TODO: Add enemy controller script with a kill command.
            
        }
    }

    private void OnDrawGizmos()
    {
        //Drawing a collider gizmo
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.transform.position + (Vector3)boxCollider.offset, boxCollider.size);
    }
}
