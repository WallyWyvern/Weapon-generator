using UnityEngine;

public class Timer
{
    public float timeLeft;
    private float duration;
    private System.Action onFinished;
    public Timer(float _duration, System.Action _onFinished)
    {
        duration = _duration;
        onFinished = _onFinished;
        timeLeft = duration;

        EventManager.instance.onTickTimers += Tick;
    }

    public void Tick(float delta) 
    {
        if (timeLeft > 0) 
        {
            timeLeft -= delta;
            if (timeLeft <= 0) 
            { 
                onFinished?.Invoke();
                EventManager.instance.onTickTimers -= Tick;
            }
        }
    }

    public void Reset(float _duration) 
    {
        if (timeLeft <= 0)
        {
            EventManager.instance.onTickTimers += Tick;
        }
        duration = _duration;
        timeLeft = duration;
        
    }

    
}
