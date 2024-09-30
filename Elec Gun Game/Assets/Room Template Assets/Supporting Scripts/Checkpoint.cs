using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private int widthInTiles = 0;

    [SerializeField]
    private int heightInTiles = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RoomDefinition room = GetComponentInParent<RoomDefinition>();
        room.SetRespawnPoint(transform.position);
    }

    //Automatically sets the size and offset of the collider surrounding the room when values are input in the inspector
    //Colldier grows from the bottom left corner so the rooms origin should be set to the bottom left tile of the room
    private void OnValidate()
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(widthInTiles, heightInTiles);
    }

    private void OnDrawGizmos()
    {
        //Keeps the collider box visible even when not selected
        DrawCheckpointBox();
    }

    private void DrawCheckpointBox()
    {
        Gizmos.color = Color.blue;
        Vector3 p1 = new Vector3(transform.position.x - (widthInTiles / 2f), transform.position.y - (heightInTiles / 2f), transform.position.z);
        Vector3 p2 = new Vector3(transform.position.x + (widthInTiles / 2f), transform.position.y - (heightInTiles / 2f), transform.position.z);
        Vector3 p3 = new Vector3(transform.position.x + (widthInTiles / 2f), transform.position.y + (heightInTiles / 2f), transform.position.z);
        Vector3 p4 = new Vector3(transform.position.x - (widthInTiles / 2f), transform.position.y + (heightInTiles / 2f), transform.position.z);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}
