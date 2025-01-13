using UnityEngine;

public class SpawnerButton : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private float buttonCooldown = 2f; // Cooldown before the button can be reactivated
    private bool coolingDown = false;
    private float activationTime;

    public delegate void ButtonAction();
    public event ButtonAction OnButtonActivation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !coolingDown)
        {
            ActivateButton();
        }
    }

    private void ActivateButton()
    {
        Debug.Log("Button activated.");
        OnButtonActivation.Invoke();
        coolingDown = true;
        activationTime = Time.time;
    }

    private void Update()
    {
        if (coolingDown && Time.time - activationTime >= buttonCooldown)
        {
            coolingDown = false;
        }
    }
}
