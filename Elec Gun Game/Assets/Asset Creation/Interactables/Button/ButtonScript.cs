using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 HOW TO USE:
    1. In the Editor, set the object you want to control in the ButtonScript Section (Ex. Assign the turret prefab to objectControlled)
    2. Again in the editor, set the button in the script of the object you want to control (Ex. Assign the button prefab to Button in Turret)

    *Button in other elements can be left empty if you want normal controls*
 */

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private bool isPressed;
    private Vector3 originalScale;
    [SerializeField] private float weightThreshold = 1f; //Weight required for button press
    [SerializeField] private Transform buttonTop; //Top of the button, squished later

    public event EventHandler<ItemActivatedEventArgs> ButtonPressed;

    [SerializeField] private GameObject objectControlled; //Assign object you want to control with button

    void Start()
    {
        originalScale = buttonTop.localScale; //Normal size when not pressed
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check for object on top of button
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && rb.mass > weightThreshold) 
        {
            isPressed = true;
            buttonTop.localScale = new Vector3(originalScale.x, originalScale.y / 2, originalScale.z);
            OnButtonPressed(new ItemActivatedEventArgs(isPressed));
            Debug.Log("Button pressed");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Check for object off of top of button
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && rb.mass > weightThreshold)
        {
            isPressed = false;
            Debug.Log("Button released");
            buttonTop.localScale = originalScale;
            OnButtonPressed(new ItemActivatedEventArgs(isPressed));
        }
    }

    protected virtual void OnButtonPressed(ItemActivatedEventArgs e) //Used to send event to other items
    {
        ButtonPressed?.Invoke(this, e);
    }
}
