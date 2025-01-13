using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] float buttonCooldown;
    private bool coolingDown = false;
    private float activationTime;
    private Animator animator;

    public delegate void ButtonAction();
    public event ButtonAction OnButtonActivation;

     private void Awake()
    {
        // Assign the Animator component
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on ButtonController GameObject.");
        }
    }

    public void onButtonTrigger()
    {
        //this will trigger when a button is hit with a projectile
        if(OnButtonActivation != null && coolingDown == false)
        {
            Debug.Log("Button Activated");
            OnButtonActivation.Invoke();
            coolingDown = true;
            activationTime = Time.time;
        }
        if (animator != null)
            {
                animator.SetTrigger("Activated"); 
                Debug.Log("Should call activated funct in animator");
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Gun_Bullet")) {
            //if collider is hit with bullet
            onButtonTrigger();
        }
    }

    private void Update()
    {
        if(Time.time - activationTime > buttonCooldown && coolingDown == true)
        {
            coolingDown = false;
            Debug.Log("Button Ready");
        }
    }
}
