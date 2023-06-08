using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSounds : MonoBehaviour
{
    [SerializeField] AudioClip collectableSound;
    [SerializeField] AudioClip tune;

    AudioSource audioSrc;
    public bool playOnAwake = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayCollectableSound(string arg)
    {
        audioSrc.PlayOneShot(collectableSound);
    }
    public void PlayTune(string arg)
    {
   
        audioSrc.PlayOneShot(tune);
        //Invoke("TuneFinished", tune.length);
        //Play tune when player begins to move
    }
    public bool TuneFinished()
    {
        Debug.Log("Tune finished");
        audioSrc.Stop();
        return true;
    }
}
