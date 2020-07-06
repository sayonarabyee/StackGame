using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlatform : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "movingPlatform")
        {
            Debug.Log("COLLISION DETECTED");
        }
        else
        {
            //You lose mechanic
        }
    }
}
