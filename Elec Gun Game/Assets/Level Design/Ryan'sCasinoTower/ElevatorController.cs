using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public GameObject elevatorFloor;
    public GameObject elevatorRoof;
    public GameObject elevatorDoorL;
    public GameObject elevatorDoorR;
    public GameObject player;

    public float doorMoveSpeed = 2f;
    private float doorMoveDistance = 5f;
    public float elevatorRaiseAmount = 25f;

    private bool isPlayerInElevator = false;
    private bool isDoorLOpen = false;
    private bool isDoorROpen = false;
    private bool hasElevatorRisen = false;
    private int floorLevel = 0;

    private LeftDoorDetection leftDoorDetection;
    private RightDoorDetection rightDoorDetection;

    private void Start()
    {
        // Start with both doors closed, then open the left door
        isDoorLOpen = false;
        isDoorROpen = false;
        hasElevatorRisen = false;

        leftDoorDetection = FindObjectOfType<LeftDoorDetection>();
        rightDoorDetection = FindObjectOfType<RightDoorDetection>();
        StartCoroutine(OpenDoors());
    }

    public void PlayerEnteredInside()
    {
        if (!isPlayerInElevator)
        {
            isPlayerInElevator = true;
            StartCoroutine(HandleInsideTrigger());
        }
    }

    public void HandlePlayerExit(string side)
    {
        if (!hasElevatorRisen)
        {
            return;
        }

        if (side == "left" && !isDoorROpen)
        {
            StartCoroutine(OpenDoor(elevatorDoorR, doorMoveDistance));
        }
        else if (side == "right" && !isDoorLOpen)
        {
            StartCoroutine(OpenDoor(elevatorDoorL, doorMoveDistance));
        }

        hasElevatorRisen = false;
    }

    private IEnumerator HandleInsideTrigger()
    {
        yield return StartCoroutine(CloseDoors());

        yield return StartCoroutine(MoveElevator(Vector3.up * elevatorRaiseAmount, 3f));

        hasElevatorRisen = true;

        yield return StartCoroutine(OpenDoors());
    }

    private IEnumerator OpenDoor(GameObject door, float openHeight)
    {
        if ((door == elevatorDoorL && isDoorLOpen) || (door == elevatorDoorR && isDoorROpen))
        {
            yield break;
        }

        float totalDistance = 0f;
        while (totalDistance < openHeight)
        {
            float moveDistance = Mathf.Min(doorMoveSpeed * Time.deltaTime, openHeight - totalDistance);
            door.transform.Translate(Vector3.up * moveDistance);
            totalDistance += moveDistance;
            yield return null;
        }

        if (door == elevatorDoorL) isDoorLOpen = true;
        if (door == elevatorDoorR) isDoorROpen = true;
    }

    private IEnumerator CloseDoor(GameObject door, float closeHeight)
    {
        if ((door == elevatorDoorL && !isDoorLOpen) || (door == elevatorDoorR && !isDoorROpen))
        {
            yield break;
        }

        float totalDistance = 0f;
        while (totalDistance < closeHeight)
        {
            float moveDistance = Mathf.Min(doorMoveSpeed * Time.deltaTime, closeHeight - totalDistance);
            door.transform.Translate(Vector3.down * moveDistance);
            totalDistance += moveDistance;
            yield return null;
        }

        if (door == elevatorDoorL) isDoorLOpen = false;
        if (door == elevatorDoorR) isDoorROpen = false;
    }

    private IEnumerator OpenDoors()
    {
        // If both doors are already open, exit early
        if (isDoorLOpen && isDoorROpen)
        {
            yield break;
        }

        float totalDistance = 0f;

        // Continue moving both doors until they are fully open
        while (totalDistance < doorMoveDistance)
        {
            float moveDistance = Mathf.Min(doorMoveSpeed * Time.deltaTime, doorMoveDistance - totalDistance);

            // Move both doors upwards by the same distance
            if (!isDoorLOpen)
            {
                elevatorDoorL.transform.Translate(Vector3.up * moveDistance);
            }
            if (!isDoorROpen)
            {
                elevatorDoorR.transform.Translate(Vector3.up * moveDistance);
            }

            totalDistance += moveDistance;
            yield return null;
        }

        // Mark both doors as open
        isDoorLOpen = true;
        isDoorROpen = true;
    }

    private IEnumerator CloseDoors()
    {
        // If both doors are already closed, exit early
        if (!isDoorLOpen && !isDoorROpen)
        {
            yield break;
        }

        float totalDistance = 0f;

        // Continue moving both doors until they are fully closed
        while (totalDistance < doorMoveDistance)
        {
            float moveDistance = Mathf.Min(doorMoveSpeed * Time.deltaTime, doorMoveDistance - totalDistance);

            // Move both doors downwards by the same distance
            if (isDoorLOpen)
            {
                elevatorDoorL.transform.Translate(Vector3.down * moveDistance);
            }
            if (isDoorROpen)
            {
                elevatorDoorR.transform.Translate(Vector3.down * moveDistance);
            }

            totalDistance += moveDistance;
            yield return null;
        }

        // Mark both doors as closed
        isDoorLOpen = false;
        isDoorROpen = false;
    }


    private IEnumerator MoveElevator(Vector3 moveDirection, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = elevatorFloor.transform.position;

        while (elapsedTime < duration)
        {
            float step = Time.deltaTime / duration;
            elevatorFloor.transform.Translate(moveDirection * step);
            elevatorRoof.transform.Translate(moveDirection * step);
            elevatorDoorL.transform.Translate(moveDirection * step);
            elevatorDoorR.transform.Translate(moveDirection * step);
            player.transform.Translate(moveDirection * step);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

 
}
