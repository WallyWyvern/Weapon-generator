using System;
using UnityEngine;

public class EventManager
{
    public static EventManager instance;
    public EventManager() { instance = this; }

    public event Action onSendUpdateTick;
    public void SendUpdateTick()
    { 
        if (onSendUpdateTick != null) { onSendUpdateTick(); }
    }
    
}