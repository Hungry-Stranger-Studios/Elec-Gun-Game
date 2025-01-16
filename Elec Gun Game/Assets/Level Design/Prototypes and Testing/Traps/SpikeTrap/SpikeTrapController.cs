using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    [Header("Trap Values")]
    [SerializeField] private float maxLength = 1f;
    [SerializeField] private float timeExtended = 1f;
    [SerializeField] private float timeToExtension = 1f;
    [SerializeField] private float timeToRetraction = 1f;

    [Header("Trap Components")]
    [SerializeField] private DeathZone deathZone;
    [SerializeField] private Transform zoneTransform;
    [SerializeField] private ButtonController linkedButton;
    [SerializeField] private Transform spriteTransform;


    private float minLength;
    private float extensionSpeed;
    private float retractionSpeed;

    private float trapExtendedTime;
    private bool trapExtending;
    private bool trapRetracting;
    private bool trapActivated;


    private void Awake()
    {
        trapExtending = false;
        trapRetracting = false;
        trapActivated = false;
        trapExtendedTime = 0;
        minLength = zoneTransform.localScale.y;
        extensionSpeed = (maxLength - minLength) / timeToExtension;
        retractionSpeed = (maxLength - minLength) / timeToRetraction;
    
        if(linkedButton != null)
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

        if(zoneTransform == null)
        {
            Debug.LogWarning("No transform linked to trap!");
        }
    }

    private void Activate()
    {
        Debug.Log("Spike Trap Activated");
        trapExtending = true;
        trapActivated = true;
        deathZone.Enable();
    }

    private void FixedUpdate()
    {
        if (trapActivated)
        {
            SpikeTrap();
        }
    }

    private void SpikeTrap()
    {
        if (trapExtending)
        {
            //check if trap is at maxlength (done extending)
            if (zoneTransform.localScale.y >= maxLength)
            {
                //we note the time in order to leave the trap extended for a short while
                trapExtending = false;
                trapExtendedTime = Time.time;
                Debug.Log("Spike Trap Finished Extending");
                return;
            }

            //extend to the right by moving the center and changing the size
            //changing the size param increases the width on both sides of the center, therefore you need to shift the center
            //moving and changing the actual object
            zoneTransform.localPosition += new Vector3(0, extensionSpeed * Time.fixedDeltaTime / 2, 0);
            zoneTransform.localScale += new Vector3(0, extensionSpeed * Time.fixedDeltaTime, 0);

            spriteTransform.localPosition += new Vector3(0, extensionSpeed * Time.fixedDeltaTime, 0);
        }

        //In order for the trap to start retracting it must:
        //be extended (greater size than start) AND no longer be extending
        if (trapExtending == false && zoneTransform.localScale.y >= minLength)
        {
            //been the above conditions for a certain length of time
            if ((Time.time - trapExtendedTime >= timeExtended) && !trapRetracting)
            {
                trapRetracting = true;
                Debug.Log("Spike Trap Retracting");
            }
        }

        if (trapRetracting)
        {
            //check if trap is at minlength (done retracting)
            if (zoneTransform.localScale.y <= minLength)
            {
                trapRetracting = false;
                trapActivated = false;
                Debug.Log("Spike Trap Deactivated");
                return;
            }
            //similar logic to above
            zoneTransform.localPosition -= new Vector3(0, retractionSpeed * Time.fixedDeltaTime / 2, 0);
            zoneTransform.localScale -= new Vector3(0, retractionSpeed * Time.fixedDeltaTime, 0);

            spriteTransform.localPosition -= new Vector3(0, retractionSpeed * Time.fixedDeltaTime, 0);
        }
    }

    public float getMaxLength()
    {
        return maxLength;
    }
}
