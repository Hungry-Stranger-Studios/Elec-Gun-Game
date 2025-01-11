using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrapController : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private LayerMask affectedLayers; //Layers to be affected by the trap
    [SerializeField] private float trapDuration = 10f; //How long the trap will be on


    [Header("Trap Components")]
    [SerializeField] private ButtonController linkedButton;
    [SerializeField] private Transform parentTransform;

    private bool trapActivated = false;
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
        //you need to link the parent's transform so you know the dimensions of the trap
        if(parentTransform == null)
        {
            Debug.LogWarning("No Transform linked to trap!");
        }
    }

    private void Activate()
    {
        Debug.Log("Activated Electric Trap");
        trapActivated = true;
        trapStarted = Time.time;
        CreateDeathZone();
    }
    private void CreateDeathZone()
    {

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
