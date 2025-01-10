using System.Collections;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D lightSource; //Reference to global light
    [SerializeField] private float flickerDuration = 2f;
    [SerializeField] private int flickerCount = 10;
    [SerializeField] private float lightIntensityOn = 2f;

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform; //Reference to camera
    [SerializeField] private Transform playerTransform; //Reference to player
    [SerializeField] private Vector3 leftPanPosition;   //Camera position for left scan
    [SerializeField] private Vector3 rightPanPosition;  //Camera position for right scan
    [SerializeField] private float panDuration = 3f;    //Duration for camera pans
    [SerializeField] private float snapBackDuration = 0.5f; //Duration for snapping back to the player

    [Header("Delays")]
    [SerializeField] private float pauseAfterLeftPan = 1f; //Pause after scanning left
    [SerializeField] private float pauseAfterPlayerFocus = 1f; //Pause after snapping back to the player
    [SerializeField] private float pauseOnEnemy = 1f; //Pause on enemy

    [Header("Entities")]
    [SerializeField] private GameObject chasingEnemy; //Activate enemy movement

    public void StartCutsceneLight()
    {
        StartCoroutine(CutsceneSequenceLight());
    }

    private IEnumerator CutsceneSequenceLight()
    {
        //Flicker the light on
        yield return StartCoroutine(FlickerLightOn());

        //Pan the camera to the left
        yield return StartCoroutine(PanCamera(leftPanPosition, panDuration));

        //Pause after scanning the left side
        yield return new WaitForSeconds(pauseAfterLeftPan);

        //Snap back to the player
        yield return StartCoroutine(PanCamera(playerTransform.position, snapBackDuration));

        //Pause on the player
        yield return new WaitForSeconds(pauseAfterPlayerFocus);

        //Pan to the right
        yield return StartCoroutine(PanCamera(rightPanPosition, panDuration));

        //Pause on enemy
        yield return new WaitForSeconds(pauseOnEnemy);

        //Play cutscene for monster roar (NEED TO IMPLEMENT)
    }

    private IEnumerator FlickerLightOn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < flickerDuration)
        {
            lightSource.intensity = Random.Range(0f, lightIntensityOn); //Random flicker
            elapsedTime += flickerDuration / flickerCount;
            yield return new WaitForSeconds(flickerDuration / flickerCount);
        }

        lightSource.intensity = lightIntensityOn; //Final intensity
    }

    private IEnumerator PanCamera(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = cameraTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cameraTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = targetPosition; // Ensure final position
    }
}

