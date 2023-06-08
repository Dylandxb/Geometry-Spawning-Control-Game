using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapTargetMovement : MonoBehaviour
{
    private float height = 6.0f;                    //moves up and down 6 points in the y
    private float frequency = 0.5f;                 //Lower frequency moves the target slower
    Vector3 targetPos;
    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(targetPos.x, Mathf.Sin(Time.time * frequency) * height + targetPos.y, 0); //Sin function to increase the targetpos.y by the height over period of movement        
    }
}
