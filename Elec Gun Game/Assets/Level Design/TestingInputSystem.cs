using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{

    private Rigidbody sphereRigidbody;
//comment
    private void Awake()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
    } 
}

//test
