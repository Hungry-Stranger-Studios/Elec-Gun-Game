using UnityEngine;

public class PlugManagement : MonoBehaviour
{
    public PlugManager plugMan;
    private InsideElevatorLogic elevatorLogic;

    private void Start()
    {
        // Find and reference the InsideElevatorLogic
        elevatorLogic = FindObjectOfType<InsideElevatorLogic>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plug"))
        {
            Destroy(other.gameObject);
            plugMan.plugCount++;
            Debug.Log("Plug obtained");

            // Reset elevator usage when a plug is deleted
            if (elevatorLogic != null)
            {
                elevatorLogic.ResetElevatorUsage();
            }
        }
    }
}
