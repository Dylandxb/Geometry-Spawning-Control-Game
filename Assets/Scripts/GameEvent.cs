using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> listeners = new List<GameEventListener>();
    public void Raise(string arg)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(arg);
        }
    }
    public void Raise()
    {
        Raise("");
    }
    public void RegisterListener(GameEventListener l)
    {
        listeners.Add(l);
    }
    public void UnRegisterListener(GameEventListener l)
    {
        listeners.Remove(l);
    }
}
