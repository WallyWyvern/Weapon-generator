using System.Collections.Generic;
using UnityEngine;

public interface IEffect : IHandler
{ 
    void UpdateEffect();
}

public interface IHandler
{
    IHandler NextHandler { get; set; }
    void Handle(List<IEffect> activeEffects, IStatusEffectable owner);
}

public abstract class BaseStatusEffect : IEffect
{
    protected float duration;
    public IHandler NextHandler { get; set; }

    public BaseStatusEffect(int intensity) { 
        CalculateStatusEffect(intensity);
        //Debug.Log(intensity);
    }

    protected abstract void CalculateStatusEffect(int intensity);

    public void Handle(List<IEffect> activeEffects, IStatusEffectable owner)
    { 
        UpdateEffect();
        NextHandler?.Handle(activeEffects, owner);
    }

    public abstract void UpdateEffect();
}

public class FireStatusEffect : BaseStatusEffect
{
    private float tickRate; 

    public FireStatusEffect(int intensity) : base(intensity) {
        
    }

    public override void UpdateEffect()
    {
        Debug.Log("I have updated the fire statis effect");
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        Debug.Log("im calculating the fire status effect");
    }
}
