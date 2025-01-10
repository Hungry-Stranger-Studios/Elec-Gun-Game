using UnityEngine;

public class RightDoorDetection : MonoBehaviour
{
    private ElevatorController elevatorController;
    private bool isExitHandled = false; // Ensures only one side processes the exit

    private void Start()
    {
        elevatorController = FindObjectOfType<ElevatorController>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isExitHandled)
        {
            isExitHandled = true; // Mark exit as handled
            elevatorController.HandlePlayerExit("right");
        }
    }

    public void ResetExit()
    {
        isExitHandled = false; // Reset when elevator logic resets
    }
}
