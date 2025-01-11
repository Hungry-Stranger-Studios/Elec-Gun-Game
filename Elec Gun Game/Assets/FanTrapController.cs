using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrapController : MonoBehaviour
{
    [Header("Fan Settings")]
    [SerializeField] private Vector2 blowDirection = Vector2.right; // Direction of the fan
    [SerializeField] private float maxForce = 10f; // Maximum force at the closest point
    [SerializeField] private float minForce = 2f; // Minimum force at the farthest point
    [SerializeField] private float blowZoneRadius = 5f; // Effective radius of the fan's blow zone
    [SerializeField] private LayerMask affectedLayers; // Layers to be affected by the fan
    [SerializeField] private float fanDuration = 10f; //How long the fan will blow for

    private List<Rigidbody2D> affectedObjects = new List<Rigidbody2D>();

    [Header("Trap Components")]
    [SerializeField] private ButtonController linkedButton;

    private bool fanBlowing = false;
    private float startedBlowing = 0;

    private void Awake()
    {
        if (linkedButton != null)
        {
            linkedButton.OnButtonActivation += Activate;
        }
        else
        {
            Debug.LogWarning("No button linked to trap!");
        }
    }

    private void Activate()
    {
        Debug.Log("Activated Fan Trap");
        fanBlowing = true;
        startedBlowing = Time.time;
    }

    private void FixedUpdate()
    {
        if (fanBlowing)
        {
            Debug.Log("She blowing");
            //Check if fan should stop blowing
            if(Time.time - startedBlowing > fanDuration)
            {
                Debug.Log("Deactivated Fan Trap");
                fanBlowing = false;
                return;
            }
            foreach (var rigidbody in affectedObjects)
            {
                if (rigidbody == null) continue;

                //Calculate distance from the fan to the object
                Vector2 directionToTarget = rigidbody.position - (Vector2)transform.position;
                float distance = directionToTarget.magnitude;

                //If the object is outside the blowZone, skip it
                if (distance > blowZoneRadius) continue;

                //calculate force based on distance
                float forceMagnitude = Mathf.Lerp(maxForce, minForce, distance/blowZoneRadius);

                //apply force in the blow direction
                Vector2 force = blowDirection.normalized * forceMagnitude;
                rigidbody.AddForce(force);
            }
        }
    }
    //add the affected object to the list upon it entering the collider's zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the object is on the affected layer
        //Note: 1 << layer is a good way to isolate the layer of the gameObject. It will be all 0's and 1 on that layer
        if((1<<collision.gameObject.layer & affectedLayers) != 0)
        {
            Rigidbody2D newBody = collision.GetComponent<Rigidbody2D>();
            if (newBody != null && !affectedObjects.Contains(newBody))
            {
                affectedObjects.Add(newBody);
            }
        }
    }
    //remove the affected object from the list upon it leaving
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & affectedLayers) != 0)
        {
            Rigidbody2D newBody = collision.GetComponent<Rigidbody2D>();
            if (newBody != null && affectedObjects.Contains(newBody))
            {
                affectedObjects.Remove(newBody);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, blowZoneRadius);
    }
}
