using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public PlayerMovement playerMovementScript;
    public LevelStateController levelStateController;
    public ChasingEnemyScript chasingEnemyScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kill player if hit, reset level and continue
        {
            playerMovementScript.KillPlayer();
            levelStateController.ResetLevel();
            chasingEnemyScript.isEnabled = true;
        }
    }
}
