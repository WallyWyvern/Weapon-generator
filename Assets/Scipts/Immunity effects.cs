using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseImmunityEffect : IEffect
{
    public IHandler nextHandler { get; set; }
    public bool active { get; set; } // unused

    protected List<IEffect> foundEffects = new List<IEffect>();

    public virtual void Handle(List<IEffect> activeEffects, IStatusEffectable owner)
    {
        foreach (var effect in foundEffects)
        {
            effect.active = false;
        }
        nextHandler?.Handle(activeEffects, owner);
    }

    // unused
    public void UpdateEffect()
    {
        
    }
}

public class FireImmunityEffect : BaseImmunityEffect
{
    public override void Handle(List<IEffect> activeEffects, IStatusEffectable owner)
    {
        foreach (var effect in activeEffects)
        {
            if (effect is FireStatusEffect)
            {
                foundEffects.Add(effect);
            }
        }
        base.Handle(activeEffects, owner);
    }
}