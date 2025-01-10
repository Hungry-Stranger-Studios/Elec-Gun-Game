using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallScript : MonoBehaviour
{
    [SerializeField] private GameObject player; //Player reference
    [SerializeField] private float velocityThreshold = 10f; //Threshold velocity to break the wall
    [SerializeField] private GameObject brokenWallPrefab; //IMPLEMENT
    [SerializeField] private bool dashRequired = false; //If you need to dash to break or just will break with velocity

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the other object is the player or has a Rigidbody2D
        Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            // Check if the player's velocity exceeds the threshold
            if (dashRequired)
            {
                if (playerRigidbody.velocity.magnitude >= velocityThreshold && WasDashRecentlyPressed())
                {
                    BreakWall();
                }
            } 
            else
            {
                if (playerRigidbody.velocity.magnitude >= velocityThreshold)
                {
                    BreakWall();
                }
            }
        }
    }

    private bool WasDashRecentlyPressed()
    {
        // Assume the player has a script called "PlayerController" with a method to get the last dash time
        PlayerMovement playerController = player.GetComponent<PlayerMovement>();
        if (playerController != null)
        {
            float timeSinceDash = Time.time - playerController.GetLastDashTime();
            return timeSinceDash <= 0.1f;
        }
        return false;
    }

    private void BreakWall()
    {
        if (brokenWallPrefab != null)
        {
            //Instantiate(brokenWallPrefab, transform.position, transform.rotation); //Broken wall sprite
            //ADD ANIMATION FOR WALL BREAKING
        }

        // Destroy the wall object
        gameObject.SetActive(false);
    }
}
