using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectricTrapController : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private float trapDuration = 10f; //How long the trap will be on
    [SerializeField] private DeathZone deathZone;

    [Header("Trap Components")]
    [SerializeField] private ButtonController linkedButton;

    private float trapStarted = 0;

    private void Awake()
    {
        if (linkedButton != null)
        {
            linkedButton.OnButtonActivation += Activate;
        }
        else
        {
            Debug.LogWarning("No button linked to trap!");
        }
        
        if(deathZone != null)
        {
            deathZone.Disable();
        }
        else
        {
            Debug.LogWarning("No DeathZone linked to trap!");
        }
    }

    private void Activate()
    {
        Debug.Log("Activated Electric Trap");
        trapStarted = Time.time;
        deathZone.Enable();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
