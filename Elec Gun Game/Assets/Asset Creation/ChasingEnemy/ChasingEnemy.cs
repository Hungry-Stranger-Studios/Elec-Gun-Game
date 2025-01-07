using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChasingEnemyScript : MonoBehaviour
{
    public PlayerMovement playerMovementScript;
    public LevelStateController levelStateController;
    public bool isEnabled;
    [SerializeField] private float enemySpeed = 5f;

    private void Update()
    {
        if (isEnabled)
        {
            transform.Translate(Vector3.left * Time.deltaTime * enemySpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kill player if hit, reset level and continue
        {
            playerMovementScript.KillPlayer();
            isEnabled = false;
            levelStateController.ResetLevel();
            isEnabled = true;
        }
        else // Destroy objects as it goes (except ground)
        {
            int includedLayer = LayerMask.NameToLayer("LevelElements");

            if (collision.gameObject.layer != includedLayer) //Keep ground, checkpoints and respawn points
            {
                return; 
            }

            collision.gameObject.SetActive(false); //Hide object if its "destroyed" by the enemy
        }
    }
}
