using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator camAnim;
    public void CollCamShake()
    {
        camAnim.SetTrigger("CamShake");
    }
}
