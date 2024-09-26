using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private Canvas pauseMenu;

    public bool paused = false;
    private GameObject[] pausedObjects;

    private void Start()
    {
        pauseMenu.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            paused = true;
            pauseMenu.enabled = true;
            puase();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            paused = false;
            pauseMenu.enabled = false;
            unpause();
        }
    }

    private void unpause()
    {
        Debug.LogWarning("Unpausing entities with the pausable script not implemented yet");
    }

    private void puase()
    {
        Debug.LogWarning("Pausing entities with the pausable script not implemented yet");
    }
}
