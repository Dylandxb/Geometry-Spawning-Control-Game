using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTargetMove : MonoBehaviour
{
    private float horizontal = 10.0f;                    //moves up and down 6 points in the y
    private float frequency = 0.5f;                 //Lower frequency moves the target slower
    Vector3 targetPos;
    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time * frequency) * horizontal + targetPos.x, targetPos.y, 0); //Sin function to increase the targetpos.y by the height over period of movement        
    }
}
