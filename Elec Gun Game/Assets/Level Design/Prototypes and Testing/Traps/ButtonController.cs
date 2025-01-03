using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject listeningTrap;
    [SerializeField] Collider2D buttonCollider;


    public delegate void ButtonAction();
    public static event ButtonAction OnButtonActivation;

    public void onButtonTrigger()
    {
        //this will trigger when a button is hit with a projectile
        if(OnButtonActivation != null)
        {
            OnButtonActivation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Gun_Bullet")) {
            //if collider is hit with bullet
            onButtonTrigger();
        }
    }
}
