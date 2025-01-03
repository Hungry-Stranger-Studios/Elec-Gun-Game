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
#pragma warning disable CS0067
public class ButtonScript : MonoBehaviour
{
    [SerializeField] private float weightThreshold = 1f; //Weight required for button press
    [SerializeField] private GameObject objectControlled; //Assgned to object wanting to control

    //Button State
    private bool isPressed = false;

    //Visual Changes
    private Vector3 originalScale;
    [SerializeField] private GameObject buttonTop; //Top of the button, squished later
    [SerializeField] private Sprite[] buttonSprites = new Sprite[2];
    SpriteRenderer buttontopRenderer;

    //Event Changes
    public event EventHandler<ItemActivatedEventArgs> ButtonPressed;
    private IControllable controlledObject; //Interface reference to pass state
    public ItemActivator activator;

    void Start()
    {
        Transform buttontopTransform = buttonTop.GetComponent<Transform>();
        buttontopRenderer = buttonTop.GetComponent<SpriteRenderer>();
        originalScale = buttontopTransform.localScale; //Normal size when not pressed

        //Try to get IControllable from objectControlled if it implements the interface
        activator.controllableItem = objectControlled.GetComponent<IControllable>();
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

    private void UpdateVisual()
    {
        buttontopRenderer.sprite = isPressed ? buttonSprites[1] : buttonSprites[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && rb.mass > weightThreshold && !isPressed)
        {
            isPressed = true;
            activator.ActivateItem(isPressed);
            UpdateVisual();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && rb.mass > weightThreshold && isPressed)
        {
            isPressed = false;
            activator.ActivateItem(isPressed);
            UpdateVisual();
        }
    }

}
#pragma warning restore CS0067
