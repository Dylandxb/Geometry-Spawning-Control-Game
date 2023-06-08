using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliders : MonoBehaviour
{
    public Camera cam;
    public BoxCollider2D boxColl;

    void Start()
    {
        //cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //boxColl = GameObject.Find("ScreenCollider").GetComponent<BoxCollider2D>();
        var aspect = (float)Screen.width / Screen.height;
        var orthoSize = cam.orthographicSize;

        var width = 2.0f * orthoSize * aspect;
        var height = 2.0f * cam.orthographicSize;

        boxColl.size = new Vector2(width, height);
        //Increase thickness of colliders
    }
}
