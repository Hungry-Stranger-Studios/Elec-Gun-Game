using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Level Template")
            {
                SceneManager.LoadScene(sceneName: "Intro");
            }

            if (currentScene.name == "Intro")
            {
                SceneManager.LoadScene(sceneName: "Level Template");
            }

        }
    }
}
