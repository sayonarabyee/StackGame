using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed = 5f;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, platformSpeed * Time.deltaTime);
        platformStop();
       
    }
    void platformStop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            platformSpeed = 0;
            //BreakPlatform()
        }
    }

    void BreakPlatform()
    {
        //Double mechanic
    }
}
