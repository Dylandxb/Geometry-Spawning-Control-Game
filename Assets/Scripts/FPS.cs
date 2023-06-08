using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class FPS : MonoBehaviour
{
    //public TextMeshProUGUI text;
    private int modularSmooth = 10;
    private float[] times;
    private int currentTime;
    void Start()
    {
        times = new float[modularSmooth];

    }
    public float DisplayFPS()
    {
        times[currentTime] = Time.deltaTime;                        //Adds current values time to the array
        currentTime = (currentTime + 1) % modularSmooth;            //If the time is incremented out of the array (+1) then the modular of 10 is used to bring it back to 0
        float averageTime = 0.0f;
        foreach (float t in times)
        {
            averageTime += t;
        }
        averageTime /= times.Length;
        float fps = 1.0f / averageTime;                          //Creates new FPS variable
        int fpsInt = (int)(fps + 0.5f);                             //Converts the fps from a float to an int and adds +0.5f to round it up
        if (currentTime < averageTime)                              //Modular value of current time is lower than the average time
        {
            Debug.Log(" FPS: " + fpsInt);  //Sets the string text of the Text component to the Count + FPS. Update this Text only once every time the array wraps around

        }
        return fpsInt;
    }

}
