using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour, IControllable
{
    [SerializeField] private float boostStrength = 10f;
    [SerializeField] private bool canBoost = false;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D padLight;

    public ButtonScript button;

    private void Start()
    {
        if (button != null)
        {
            button.ButtonPressed += OnActivation;
        }
    }

    private void Update()
    {
        if (canBoost) 
        { 
            padLight.enabled = true;
        } else
        {
            padLight.enabled = false;
        }
    }

    // Handle bullet impacts and player collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun_Bullet"))
        {
            canBoost = !canBoost; // Toggle boosting ability on bullet collision
        }

        // Check if the object has a Rigidbody
        if (canBoost)
        {
            var playerScript = other.GetComponent<PlayerMovement>();
            if (playerScript != null)
            {
                playerScript.BoostUpwards(boostStrength);
            }
        }
    }
    public void OnActivation(object sender, ItemActivatedEventArgs e)
    {
        // Update booster's state based on the button's activation state
        canBoost = e.isActive;
    }

    // Clean up event subscription to avoid memory leaks
    void OnDestroy()
    {
        if (button != null)
        {
            button.ButtonPressed -= OnActivation;
        }
    }
}
