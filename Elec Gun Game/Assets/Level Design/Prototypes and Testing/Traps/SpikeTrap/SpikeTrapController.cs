using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    [Header("Trap Values")]
    [SerializeField] private float maxLength;
    [SerializeField] private float trapFrames;
    private float minLength;
    private float frameIncrement;

    [Header("Trap Components")]
    [SerializeField] private BoxCollider2D deathZone;
    [SerializeField] private Sprite trapSprite;

    private bool trapMoving;

    private void Awake()
    {
        trapMoving = false;
        //frameIncrement represents how much the box moves per frame
        minLength = deathZone.size.x;
        frameIncrement = (maxLength - minLength) / trapFrames;

    }

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
        trapMoving = true;
    }

    private void Update()
    {
        if(trapMoving)
        {
            //extend to the right by moving the center and changing the size
            deathZone.offset += new Vector2(frameIncrement/2, 0);
            deathZone.size += new Vector2(frameIncrement, 0);
        }
        if(deathZone.size.x >= maxLength) 
        {
            trapMoving = false;
        }
    }
    
}
