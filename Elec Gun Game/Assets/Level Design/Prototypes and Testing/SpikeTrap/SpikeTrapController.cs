using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    [Header("Trap Values")]
    [SerializeField] private float trapLength = 0;
    [SerializeField] private float trapSpeed = 0;

    [Header("Trap Components")]
    [SerializeField] private BoxCollider2D deathZone;
    [SerializeField] private Sprite trapSprite;

    private void OnEnable()
    {
        ButtonController.OnButtonActivation += Activate;
    }

    private void OnDisable()
    {
        ButtonController.OnButtonActivation -= Activate;
    }

    private void Activate()
    {
        Debug.Log("Hit!");
    }
    
}
