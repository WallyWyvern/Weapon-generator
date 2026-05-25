using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{ 

}

public interface IHandler
{
    IEffect NextHandler { get; set; }
    void Handle(ref Dictionary<EffectType, int> damageDict);
}

public abstract class BaseStatusEffect
{
    protected float duration;
    public BaseStatusEffect(int intensity) { 
        CalculateStatusEffect(intensity);
        Debug.Log(intensity);
    }

    protected abstract void CalculateStatusEffect(int intensity);
    protected abstract void ApplyStatus();

}

public class FireStatusEffect : BaseStatusEffect
{
    private float tickRate; 

    public FireStatusEffect(int intensity) : base(intensity) {
        
    }

    protected override void ApplyStatus()
    {
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        Debug.Log("im calculating the status effect");
    }
}
