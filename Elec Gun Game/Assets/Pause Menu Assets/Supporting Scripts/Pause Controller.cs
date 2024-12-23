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
    private GameObject player;

    private void Start()
    {
        pauseMenu.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            paused = true;
            pauseMenu.enabled = true;
            player.GetComponent<PlayerMovement>().DeactivePlayer();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            paused = false;
            pauseMenu.enabled = false;
            player.GetComponent<PlayerMovement>().ActivatePlayer();
        }
    }
}
