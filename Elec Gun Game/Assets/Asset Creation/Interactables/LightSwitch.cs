using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float interactionRange = 5.0f;
    [SerializeField] private CutsceneController cutsceneController;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerInRange())
        {
            cutsceneController.StartCutsceneLight();
        }
    }

    private bool IsPlayerInRange()
    {
        return (transform.position - player.transform.position).sqrMagnitude < interactionRange;
    }
}
