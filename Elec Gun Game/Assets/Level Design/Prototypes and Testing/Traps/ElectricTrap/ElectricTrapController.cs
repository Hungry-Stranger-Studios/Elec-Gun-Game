using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectricTrapController : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private float trapDuration = 10f; //How long the trap will be on
    [SerializeField] private DeathZone deathZone;

    [Header("Trap Components")]
    [SerializeField] private ButtonController linkedButton;

    private float trapStarted = 0;
    private bool trapActivated = false;

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
        
        if(deathZone != null)
        {
            deathZone.Disable();
        }
        else
        {
            Debug.LogWarning("No DeathZone linked to trap!");
        }
    }

    private void Activate()
    {
        Debug.Log("Activated Electric Trap");
        trapStarted = Time.time;
        deathZone.Enable();
        trapActivated = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (trapActivated)
        {
            if (Time.time - trapStarted > trapDuration)
            {
                Debug.Log("Deactivated Electric Trap");
                deathZone.Disable();
                trapActivated = false;
            }
        }
    }
}
