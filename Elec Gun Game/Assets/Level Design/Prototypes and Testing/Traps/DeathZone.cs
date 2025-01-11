using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DeathZone : MonoBehaviour
{
    [Header("DeathZone Components")]
    [SerializeField]    private LayerMask affectedLayers;
    [SerializeField]    private BoxCollider2D zoneCollider;

    public DeathZone(LayerMask affectedLayers, BoxCollider2D newZoneCollider)
    {
        this.affectedLayers = affectedLayers;
        
        BoxCollider2D existingCollider = GetComponent<BoxCollider2D>();
        //Destroy existing collider if there already is one
        if(existingCollider != null)
        {
            Destroy(existingCollider);
        }
        //add new collider to the deathZone component
        gameObject.AddComponent<BoxCollider2D>();
        if(gameObject.GetComponent<BoxCollider2D>() == null)
        {
            Debug.LogAssertion("Failed to create new collider");
            return;
        }
        zoneCollider = gameObject.GetComponent<BoxCollider2D>();

        zoneCollider.enabled = true;
        zoneCollider.isTrigger = true;
        zoneCollider.includeLayers = affectedLayers;
        zoneCollider.size = newZoneCollider.size;
        zoneCollider.offset = newZoneCollider.offset;
    }

    public DeathZone() 
    {
        affectedLayers = GetComponent<LayerMask>();
        zoneCollider = GetComponent<BoxCollider2D>();
    }

    private void Awake()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the object being killed is a player
        if ((1 << collision.gameObject.layer & affectedLayers) !=  0) {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerMovement>().KillPlayer();
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                //TODO: Add enemy controller script with a kill command.

            }
        }
    }

    private void OnDrawGizmos()
    {
        //Drawing a collider gizmo
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }
}
