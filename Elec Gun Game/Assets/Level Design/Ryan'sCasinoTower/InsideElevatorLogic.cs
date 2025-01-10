using UnityEngine;

public class InsideElevatorLogic : MonoBehaviour
{
    private ElevatorController elevatorController;

    private void Start()
    {
        // Find and reference the ElevatorController
        elevatorController = FindObjectOfType<ElevatorController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorController.PlayerEnteredInside();
        }
    }
}
