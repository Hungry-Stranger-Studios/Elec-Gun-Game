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
            Transform player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            Transform camera_follow_obj = player.Find("CAMERA_FOLLOW_OBJECT");

            cam.Follow = camera_follow_obj;
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
