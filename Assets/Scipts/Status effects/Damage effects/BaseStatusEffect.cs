using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStatusEffect : IEffect
{
    public IHandler nextHandler { get; set; }
    public bool active { get; set; }

    protected float duration;
    protected int intensity;
    protected IStatusEffectable owner;
    protected Timer timer;
    

    public BaseStatusEffect(int _intensity) 
    { 
        intensity = _intensity;
    }

    public virtual void Handle(List<IEffect> activeEffects, IStatusEffectable _owner)
    { 
        owner = _owner;
        owner.activeEffects.Remove(this);
        CalculateStatusEffect(intensity);
        UpdateEffect();
        nextHandler?.Handle(activeEffects, owner);
    }

    public abstract void UpdateEffect();

    protected abstract void CalculateStatusEffect(int intensity);
}
