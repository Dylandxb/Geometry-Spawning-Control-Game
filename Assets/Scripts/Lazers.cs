using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazers : MonoBehaviour
{
    public SpriteRenderer beamRend;
    private DecimalConverter decConvert;
    void Start()
    {
        beamRend = GetComponent<SpriteRenderer>();
        decConvert = GameObject.Find("Decimals").GetComponent<DecimalConverter>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            beamRend.color = decConvert.GetColorFromString("FFEC00FF"); //Use dec convert to keep same yellow colour but increase alpha

            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            beamRend.color = decConvert.GetColorFromString("FFEC00B3");
        }
    }
}

