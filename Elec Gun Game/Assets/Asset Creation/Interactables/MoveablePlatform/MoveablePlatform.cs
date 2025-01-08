using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [Header("Path Settings")]
    [SerializeField] private Transform[] pathPoints; //Set points that the platform goes to
    [SerializeField] private float moveSpeed = 2f;   //Speed of platform
    [SerializeField] private bool loop = true;       //Keeps going after one path
    [SerializeField] private bool pingPong = false;  //Whether the platform moves back and forth

    [Header("Platform Settings")]
    [SerializeField] private Vector3 platformSize = new Vector3(2f, 0.5f, 1f); //Adjustable size of platform

    private int currentTargetIndex = 0; //Current target point in the path
    private int direction = 1;         //Movement direction for Ping-Pong

    private void Start()
    {
        //Initial size of platform
        transform.localScale = platformSize;
    }

    private void Update()
    {
        if (pathPoints.Length == 0) return; //Skip if no path points are defined (Ordinary platform)

        //Move towards the current target point
        Transform targetPoint = pathPoints[currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        //Check if the platform has reached the current target point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            //Update target point based on movement mode
            if (pingPong)
            {
                UpdatePingPongTarget();
            }
            else
            {
                UpdateLoopTarget();
            }
        }
    }

    private void UpdateLoopTarget()
    {
        currentTargetIndex += 1;

        if (currentTargetIndex >= pathPoints.Length)
        {
            if (loop)
            {
                currentTargetIndex = 0; //Loop back to first point
            }
            else
            {
                currentTargetIndex = pathPoints.Length - 1; // Stop at last point
            }
        }
    }

    private void UpdatePingPongTarget()
    {
        currentTargetIndex += direction;

        if (currentTargetIndex >= pathPoints.Length || currentTargetIndex < 0)
        {
            direction *= -1; //Reverse direction
            currentTargetIndex += direction; //Adjust index for bounce-back
        }
    }

    private void OnDrawGizmos()
    {
        //Draw the path points in the editor for visualization
        if (pathPoints != null && pathPoints.Length > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < pathPoints.Length; i++)
            {
                if (pathPoints[i] != null)
                {
                    Gizmos.DrawSphere(pathPoints[i].position, 0.2f);

                    if (i < pathPoints.Length - 1)
                    {
                        Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
                    }
                }
            }

            //Close the loop if looping is enabled
            if (loop && pathPoints.Length > 1)
            {
                Gizmos.DrawLine(pathPoints[pathPoints.Length - 1].position, pathPoints[0].position);
            }
        }

        //Draw size of platform with Gizmo
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawWireCube(transform.position, platformSize);
    }
}
