using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingScript : MonoBehaviour
{
    
    /* Made this script to try and 
    have the platform universally fit to
    the tiles, while also expanding or shrinking
    the platform's length */

    [SerializeField]
    private int length = 0;

    [SerializeField]
    private int height = 0;

    //Automatically changes the height and length of the platform 
    //(thank you evan :) )
    private void OnValidate()
    {

        gameObject.GetComponent<Transform>().position = new Vector2(1, 1)
        

        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(length, height);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f * length, 0.5f * height);
    }

}
