using UnityEngine;

public class RightDoorDetection : MonoBehaviour
{
    private ElevatorController elevatorController;

    private void Start()
    {
        elevatorController = FindObjectOfType<ElevatorController>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorController.HandlePlayerExit("right");
        }
    }
}
