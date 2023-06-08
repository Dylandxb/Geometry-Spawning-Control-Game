using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
