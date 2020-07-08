using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        var speed = this.speed * Time.deltaTime;
        transform.position += new Vector3(0, 0, speed);
    }
}
