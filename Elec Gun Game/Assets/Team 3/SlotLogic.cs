using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotLogic : MonoBehaviour
{
    public bool triggerActive;
    public bool spinning;
    public int outcome;
    public CoinManager coinMan;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            Spin();
        }
    }

    public void Spin()
    {
        if (coinMan.coinCount > 0)
        {
            Debug.Log("Slot is spinning");
            spinning = true;
            outcome = Random.Range(1, 4);
            coinMan.coinCount--;

            if (outcome == 1)
            {
                //+5 coins
                Debug.Log("Plus 5 coins");
                coinMan.coinCount = coinMan.coinCount + 5;
            }
            else if (outcome == 2)
            {
                Debug.Log("Damage to player");
                //Decrease to players health goes here
            }
            else if (outcome == 3)
            {
                //-3 coins
                Debug.Log("Lose 3 coins");
                if ((coinMan.coinCount - 3) > 0)
                {
                    coinMan.coinCount = coinMan.coinCount - 3;
                }
                else
                {
                    coinMan.coinCount = 0;
                }
                //Just like a real casino!
            }

            spinning = false;
        }
        else
        {
            Debug.Log("Spin unavailable");
        }
    }
}