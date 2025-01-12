using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerButton : MonoBehaviour
{
    //Edit this in the unity editor if you would like a cooldown. Otherwise it will only activate once.
    [SerializeField] float buttonCooldown = Mathf.Infinity;
    private bool coolingDown = false;
    private float activationTime;
    //This is for making a unity event work
    public delegate void ButtonAction();
    public event ButtonAction OnButtonActivation;

    public void onButtonTrigger()
    {
        //this will trigger when a button is hit with a projectile
        if (OnButtonActivation != null && !coolingDown)
        {
            OnButtonActivation.Invoke();
            coolingDown = true;
            activationTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PUT YOUR CUSTOM "ACTIVATOR" HERE
        //i.e., for a touch-button
        if(collision.gameObject.tag == "Player")
        {
            //use this method to activate whatever you want
            onButtonTrigger();
        }
    }

    private void Update()
    {
        //NOTE: If you do not want a cooldown, get rid of this line and the serialized field. Its a decent memory waster otherwise.
        if (Time.time - activationTime > buttonCooldown && coolingDown == true)
        {
            coolingDown = false;
        }
    }
}
