using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject respawnPosition; //Position of checkpoint
    [SerializeField] private Transform enemyPosition;  //Reference to the enemy position
    [SerializeField] private float enemyOffset = 2.0f;  //Distance to respawn enemy before checkpoint

    public List<LevelObjectState> levelObjectStates = new List<LevelObjectState>();
    private void Start()
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {

            //Skip elements that aren't destroyed (Don't need to be reset)
            if (IsPermanentObject(obj))
            {
                continue;
            }

            //Store all other objects
            levelObjectStates.Add(new LevelObjectState(obj));
        }
    }

    private bool IsPermanentObject(GameObject obj)
    {
        int excludedLayer = LayerMask.NameToLayer("LevelElements");
        return obj.layer != excludedLayer;
    }

    public void ResetLevel()
    {
        foreach (LevelObjectState objState in levelObjectStates)
        {
            if (objState.gameObject != null)
            {
                objState.gameObject.SetActive(false);  // Hide the object
            }
        }

        // Reset all objects' positions, rotations, and active states
        foreach (LevelObjectState objState in levelObjectStates)
        {
            if (objState.gameObject != null)
            {
                objState.gameObject.transform.position = objState.startPosition;
                objState.gameObject.transform.rotation = objState.startRotation;
                objState.gameObject.SetActive(objState.wasActive); // Restore original active state
            }
        }

        // Reset the enemy position relative to the checkpoint
        if (enemyPosition != null)
        {
            Vector2 checkpointPosition = respawnPosition.transform.position;
            enemyPosition.position = new Vector2(checkpointPosition.x + enemyOffset, enemyPosition.position.y);
        }
    }
}
