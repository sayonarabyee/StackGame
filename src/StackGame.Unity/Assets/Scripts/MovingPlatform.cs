using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float initialSpeed = 4;
    public bool isSpeedAxisZ;


    void Update()
    {
        var speed = initialSpeed * Time.deltaTime;
        var speedVector = new Vector3(isSpeedAxisZ ? 0 : speed, 0, isSpeedAxisZ ? speed : 0);
        transform.position += speedVector;
    }
}