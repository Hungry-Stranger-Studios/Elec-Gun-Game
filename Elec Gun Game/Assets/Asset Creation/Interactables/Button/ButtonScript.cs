using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 HOW TO USE:
    1. In the Editor, set the object you want to control in the ButtonScript Section (Ex. Assign the turret prefab to objectControlled)
    2. Again in the editor, set the button in the script of the object you want to control (Ex. Assign the button prefab to Button in Turret)

    *Button in other elements can be left empty if you want normal controls*
 */

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private bool isPressed = false;
    private Vector3 originalScale;
    [SerializeField] private float weightThreshold = 1f; //Weight required for button press
    [SerializeField] private GameObject buttonTop; //Top of the button, squished later
    SpriteRenderer buttontopRenderer;

    public event EventHandler<ItemActivatedEventArgs> ButtonPressed;
    private IControllable controlledObject; //Interface reference to pass state
    [SerializeField] private GameObject objectControlled; //Assgned to object wanting to control

    [SerializeField] private Sprite[] buttonSprites = new Sprite[2];

    void Start()
    {
        Transform buttontopTransform = buttonTop.GetComponent<Transform>();
        buttontopRenderer = buttonTop.GetComponent<SpriteRenderer>();
        originalScale = buttontopTransform.localScale; //Normal size when not pressed

        //Try to get IControllable from objectControlled if it implements the interface
        controlledObject = objectControlled.GetComponent<IControllable>();
    }

    private void Update()
    {
        if (!isPressed)
        {
            buttontopRenderer.sprite = buttonSprites[0]; //Button is on
        }
        else
        {
            buttontopRenderer.sprite = buttonSprites[1];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for object on top of button and ensure it triggers the press only once
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && rb.mass > weightThreshold && !isPressed)
        {
            isPressed = true;
            OnButtonPressed(new ItemActivatedEventArgs(isPressed));
            Debug.Log("Button pressed. Item should be " + isPressed);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check for object off the button and trigger release only once
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && rb.mass > weightThreshold && isPressed)
        {
            isPressed = false;
            OnButtonPressed(new ItemActivatedEventArgs(isPressed));
            Debug.Log("Button released. Item should be " + isPressed);
        }
    }

    protected virtual void OnButtonPressed(ItemActivatedEventArgs e) //Used to send event to other items
    {
        ButtonPressed?.Invoke(this, e);
    }
}
