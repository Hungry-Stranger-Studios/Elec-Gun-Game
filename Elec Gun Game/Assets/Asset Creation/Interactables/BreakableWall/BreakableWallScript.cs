using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallScript : MonoBehaviour
{
    [SerializeField] private float velocityThreshold = 10f; // Threshold velocity to break the wall
    [SerializeField] private GameObject brokenWallPrefab; // Prefab for the broken wall (can be an effect or a destroyed wall object)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object is the player or has a Rigidbody2D
        Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            // Check if the player's velocity exceeds the threshold
            if (playerRigidbody.velocity.magnitude >= velocityThreshold)
            {
                BreakWall();
            }
        }
    }

    private void BreakWall()
    {
        if (brokenWallPrefab != null)
        {
            //Instantiate(brokenWallPrefab, transform.position, transform.rotation); //Broken wall sprite
            //ADD ANIMATION FOR WALL BREAKING
        }

        // Destroy the wall object
        Destroy(gameObject);
    }
}
