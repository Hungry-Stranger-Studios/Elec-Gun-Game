using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject listeningTrap;
    [SerializeField] Collider2D buttonCollider;
    [SerializeField] float buttonCooldown;
    [SerializeField] Animator buttonAnimator;  // Add this line
    private bool coolingDown = false;
    private float activationTime;

    public delegate void ButtonAction();
    public event ButtonAction OnButtonActivation;

    public void onButtonTrigger()
    {
        //this will trigger when a button is hit with a projectile
        if(OnButtonActivation != null && coolingDown == false)
        {
            OnButtonActivation.Invoke();
            coolingDown = true;
            activationTime = Time.time;
        }
        if (buttonAnimator != null)
            {
                buttonAnimator.SetTrigger("Activated"); 
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
        }
    }
}
