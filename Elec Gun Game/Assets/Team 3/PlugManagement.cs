using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugManagement : MonoBehaviour
{
    public PlugManager plugMan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plug"))
        {
            Destroy(other.gameObject);
            plugMan.plugCount++;
            Debug.Log("Plug obtained");
        }
    }
}
