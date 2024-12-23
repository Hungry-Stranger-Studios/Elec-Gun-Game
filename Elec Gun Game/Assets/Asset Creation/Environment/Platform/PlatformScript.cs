using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    
    /* Made this script to try and 
    have the platform universally fit to
    the tiles, while also expanding or shrinking
    the platform's length */

    //I stole alot of evan's code for this lmao

    [SerializeField]
    private float length = 0f;

    [SerializeField]
    private float height = 0f;

    //Automatically changes the height and length of the platform 
    private void OnValidate()
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(length, height);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f * length, 0.5f * height);
    }

    
    private void OnDrawGizmos()
    {
        //Keeps the collider box visible even when not selected
        DrawColliderBox();
    }

    private void DrawColliderBox()
    {
        Gizmos.color = Color.green;
        Vector3 p1 = transform.position;
        Vector3 p2 = new Vector3(transform.position.x + length, transform.position.y, transform.position.z);
        Vector3 p3 = new Vector3(transform.position.x + length, transform.position.y + height, transform.position.z);
        Vector3 p4 = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }

}
