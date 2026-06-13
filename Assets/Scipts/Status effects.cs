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
    protected IStatusEffectable owner;
    public IHandler NextHandler { get; set; }

    public BaseStatusEffect(int intensity) { 
        CalculateStatusEffect(intensity);
        //Debug.Log(intensity);
    }

    protected abstract void CalculateStatusEffect(int intensity);

    public virtual void Handle(List<IEffect> activeEffects, IStatusEffectable _owner)
    { 
        owner = _owner;
        UpdateEffect();
        NextHandler?.Handle(activeEffects, owner);
    }

    public abstract void UpdateEffect();
}

public class RegularDamage : BaseStatusEffect
{
    private float damage;
    public RegularDamage(int intensity) : base(intensity) { }

    public override void UpdateEffect()
    {
        owner.Damage(damage);
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        damage = intensity / 10;
    }
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
