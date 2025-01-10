using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float interactionRange = 5.0f;
    [SerializeField] private CutsceneController cutsceneController;
    [SerializeField] private Sprite newSprite;
    private SpriteRenderer spriteRenderer;
    private bool isEnabled = true;
    private void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerInRange() && isEnabled)
        {
            spriteRenderer.sprite = newSprite;
            cutsceneController.StartCutsceneLight();
            isEnabled = false;
        }
    }

    private bool IsPlayerInRange()
    {
        return (transform.position - player.transform.position).sqrMagnitude < interactionRange;
    }
}
