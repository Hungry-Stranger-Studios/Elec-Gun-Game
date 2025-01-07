using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Singletons;
using UnityEngine;

public class RespawnController : Singleton<RespawnController>
{
    [SerializeField]
    private GameObject respawnPoint;

    [SerializeField]
    private GameObject fadeInOut;

    [SerializeField]
    private SpriteRenderer curtain;

    private GameObject player;
    private Transform playerCameraFollowTr;

    //Player references the singlton and calls this method to begin the respawn process
    public void BeginRespawn()
    {
        if (player != null)
        {
            fadeInOut.GetComponent<FadeInOutController>().FadeOut();    //Begin fade out animation
        }
    }

    //Called when the fade out has completed and the player can be moved while hidden by the black screen
    public void MovePlayer()
    {
        if (player != null)
        {
            player.transform.position = respawnPoint.transform.position;    //Move the player
            //Enable the curtain to hide the scene at the respawn point
            //When the camera moves to that spot there is 1 frame before the animation covering the player is moved too since its not a child
            //The curtain hides that
            curtain.enabled = true; 
        }
        
    }

    public void FadeIn()
    {
        curtain.enabled = false; //Turn off the curtain and allow the fade in animation to be played
        player.GetComponent<PlayerMovement>().ActivatePlayer(); //Give control back to the player
    }

    //Used when a checkpoint is collected shifting the respawn point mid game
    public void SetRespawnPoint(Vector3 newPos)
    {
        respawnPoint.transform.position = newPos;
        curtain.gameObject.transform.position = new Vector3(newPos.x, newPos.y - 20, curtain.gameObject.transform.position.z);
    }

    //Update keeps the fade in animation at the same position as the CAMERA_FOLLOW_OBJECT that is a child of the player
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerCameraFollowTr = player.transform.Find("CAMERA_FOLLOW_OBJECT");
        }
        if (player != null)
        {
            //Move the fade in/out obj but keep its z value the same so it can be infront of everything. Also shift it down a couple units to be more centered
            fadeInOut.transform.position = new Vector3(playerCameraFollowTr.position.x, playerCameraFollowTr.position.y - 6, fadeInOut.transform.position.z);
        }
    }
}
