using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{ 
    void UpdateEffect();
}

public interface IHandler
{
    IEffect NextHandler { get; set; }
    void Handle(List<IEffect> activeEffects, IStatusEffectable owner);
}

public abstract class BaseStatusEffect : IHandler, IEffect
{
    protected float duration;

    public IEffect NextHandler { get => NextHandler; set{ NextHandler = value; } }

    public BaseStatusEffect(int intensity) { 
        CalculateStatusEffect(intensity);
        //Debug.Log(intensity);
    }

    protected abstract void CalculateStatusEffect(int intensity);

    public abstract void Handle(List<IEffect> activeEffects, IStatusEffectable owner);

    public abstract void UpdateEffect();
}

public class FireStatusEffect : BaseStatusEffect
{
    private float tickRate; 

    public FireStatusEffect(int intensity) : base(intensity) {
        
    }

    public override void Handle(List<IEffect> activeEffects, IStatusEffectable owner)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        Debug.Log("im calculating the status effect");
    }
}
