using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class RoomDefinition : MonoBehaviour
{
    [SerializeField]
    private int widthInTiles = 0;

    [SerializeField]
    private int heightInTiles = 0;

    [SerializeField]
    private GameObject respawnPoint;

    //Player would use the referencce they have to the current room they're in to have their position reset to that rooms current respawn point
    public void Respawn(Transform player)
    {
        player.position = respawnPoint.transform.position;
    }

    //Used when a checkpoint is collected shifting the respawn point mid game
    public void SetRespawnPoint(Vector3 newPos)
    {
        respawnPoint.transform.position = newPos;
    }

    //Used to set the players current room this upon entry 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Debug.Log("SET CURRENT ROOM REFERENCE TO BE THIS ROOM");
        }
    }

    //Used to unset the players current room this upon exit 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("UNSET CURRENT ROOM REFERENCE TO BE THIS ROOM");
        }
    }

    //Automatically sets the size and offset of the collider surrounding the room when values are input in the inspector
    //Colldier grows from the bottom left corner so the rooms origin should be set to the bottom left tile of the room
    private void OnValidate()
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(widthInTiles, heightInTiles);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f * widthInTiles, 0.5f * heightInTiles);


    }

   
    private void OnDrawGizmos()
    {
        //Keeps the collider box visible even when not selected
        DrawColliderBox();
    }

    private void DrawColliderBox()
    {
        Gizmos.color = Color.red;
        Vector3 p1 = transform.position;
        Vector3 p2 = new Vector3(transform.position.x + widthInTiles, transform.position.y, transform.position.z);
        Vector3 p3 = new Vector3(transform.position.x + widthInTiles, transform.position.y + heightInTiles, transform.position.z);
        Vector3 p4 = new Vector3(transform.position.x, transform.position.y + heightInTiles, transform.position.z);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}
