using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy whatever is attached to the collider
        Destroy(collision.gameObject);
    }
}
