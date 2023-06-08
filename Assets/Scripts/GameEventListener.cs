using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent pickUp;
    [SerializeField] GameEvent tune;
    [SerializeField] UnityEvent<string> response;
    void OnEnable()
    {
        pickUp.RegisterListener(this);
        tune.RegisterListener(this);

    }
    void OnDisable()
    {
        pickUp.UnRegisterListener(this);
        tune.RegisterListener(this);
        //Stop playing tune after game cycle is complete - 3 Minute game

    }
    public void OnEventRaised(string arg)
    {
        response.Invoke(arg);
    }
}
