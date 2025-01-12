using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedSpawner : MonoBehaviour
{
    [Header("Trap Components")]
    //Make sure to link the button you want in the unity editor
    [SerializeField] private SpawnerButton linkedButton;

    //Idea: [SerializeField] private EnemySpawner spawner; //Idk what the actual enemy spawner class is.

    private void Awake()
    {
        if (linkedButton != null)
        {
            linkedButton.OnButtonActivation += Activate;
        }
        else
        {
            Debug.LogWarning("No button linked to spawner!");
        }
    }

    private void Activate()
    {
        //this is where you can put the functionality of the spawner.
        //Idea: give this class a private data member of the spawner you want to link it to. Activate the spawner from this func from here
        Debug.Log("Activated Spawner");
    }
}
