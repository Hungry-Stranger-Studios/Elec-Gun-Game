using UnityEngine;

public class InsideElevatorLogic : MonoBehaviour
{
    private ElevatorController elevatorController;
    private bool isPlayerInTriggerZone = false;
    private bool hasUsedElevator = false;

    private void Start()
    {
        // Find and reference the ElevatorController
        elevatorController = FindObjectOfType<ElevatorController>();
    }

    private void Update()
    {
        // Check if the player is in the trigger zone and presses the interaction key
        if (isPlayerInTriggerZone && Input.GetKeyDown(KeyCode.E))
        {
            if (!hasUsedElevator)
            {
                elevatorController.PlayerEnteredInside();
                hasUsedElevator = true;
            }
            else
            {
                Debug.Log("Elevator cannot be used until a plug is deleted.");
            }
        }
    }

    public void ResetElevatorUsage()
    {
        hasUsedElevator = false;
        Debug.Log("Elevator usage reset. A plug was deleted.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTriggerZone = true; // Player is now in the trigger zone
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTriggerZone = false; // Player has left the trigger zone
        }
    }
}
