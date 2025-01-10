using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrapController : MonoBehaviour
{
    [Header("Trap Values")]
    [SerializeField] private float timeBlowing;
    [SerializeField] private float maxStrength;
    [SerializeField] private Vector2 blowDirection;
    [SerializeField] private LayerMask fanLayerMask;

    [Header("Trap Components")]
    [SerializeField] private ButtonController linkedButton;

    private bool fanBlowing = false;

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
    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
     }

    private void OnDrawGizmos()
    {
        if (blowZone != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(((Vector3)blowZone / 2) + this.transform.position, (Vector3)blowZone);
        }
    }
}
