using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed = 5f;
    public bool isDirectionAxisZ = true;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        var speed = platformSpeed * Time.deltaTime;
        transform.position += new Vector3(0, !isDirectionAxisZ ? speed : 0, isDirectionAxisZ ? speed : 0);
    }
}
