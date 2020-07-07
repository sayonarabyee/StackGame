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
    }
}
