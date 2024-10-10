using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    // Start is called before the first frame update


    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        if (cam.Follow == null)
        {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            
            cam.Follow = player.transform;
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
