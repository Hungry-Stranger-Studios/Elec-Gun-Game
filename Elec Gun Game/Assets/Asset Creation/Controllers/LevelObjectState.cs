using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelObjectState
{
    public GameObject gameObject;
    public Vector3 startPosition;
    public Quaternion startRotation; //Object rotation
    public bool wasActive;           //Store if object was active or inactive

    public LevelObjectState(GameObject obj)
    {
        gameObject = obj;
        startPosition = obj.transform.position;
        startRotation = obj.transform.rotation;
        wasActive = obj.activeSelf;
    }
}
