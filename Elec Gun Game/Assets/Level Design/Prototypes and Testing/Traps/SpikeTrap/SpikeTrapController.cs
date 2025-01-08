using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    [Header("Trap Values")]
    [SerializeField] private float maxLength;
    [SerializeField] private float trapSpeed;
    [SerializeField] private float minLength;

    [Header("Trap Components")]
    [SerializeField] private BoxCollider2D deathZone;
    [SerializeField] private Sprite trapSprite;

    private bool trapMoving;
    private float deathZone_ScaleX, deathZone_ScaleY;

    private void Awake()
    {
        trapMoving = false;
    }

    private void OnEnable()
    {
        ButtonController.OnButtonActivation += Activate;
    }

    private void OnDisable()
    {
        ButtonController.OnButtonActivation -= Activate;
        trapMoving = false;
    }

    private void Activate()
    {
        Debug.Log("Hit!");
        trapMoving = true;
    }

    private void Update()
    {
        if(trapMoving)
        {
            deathZone.size += 
        }
    }
    
}
