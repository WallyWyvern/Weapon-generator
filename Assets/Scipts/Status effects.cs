using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStatusEffect : IEffect
{
    public IHandler nextHandler { get; set; }

    protected float duration;
    protected int intensity;
    protected IStatusEffectable owner;
    protected Timer timer;
    protected bool active = false;

    public BaseStatusEffect(int _intensity) 
    { 
        intensity = _intensity;
    }

    public virtual void Handle(List<IEffect> activeEffects, IStatusEffectable _owner)
    { 
        owner = _owner;
        active = true;
        CalculateStatusEffect(intensity);
        UpdateEffect();
        nextHandler?.Handle(activeEffects, owner);
    }

    public abstract void UpdateEffect();

    protected abstract void CalculateStatusEffect(int intensity);
}

public class RegularDamage : BaseStatusEffect
{
    public float damage;

    public RegularDamage(int intensity) : base(intensity) { }

    public override void UpdateEffect()
    {
        owner.Damage(damage);
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        damage = intensity / 10f;
    }
}

public class FireStatusEffect : BaseStatusEffect
{
    private float tickRate;
    private float damage;

    public FireStatusEffect(int intensity) : base(intensity) { }

    public override void UpdateEffect()
    {
        if (duration <= 0)
        {
            active = false;
            CalculateStatusEffect(intensity);
        } 
        if (!active) return;
        if (timer == null) timer = new Timer(tickRate, UpdateEffect); else timer.Reset(tickRate);
        owner.Damage(damage);
        duration -= tickRate;
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        tickRate = 20f / intensity;
        duration = intensity * 0.05f;
        damage = intensity * 0.1f;
    }
}

public class PoisonStatusEffect : BaseStatusEffect
{
    private float tickRate = 0.5f;
    private float damage = 5f;
    public PoisonStatusEffect(int intensity) : base(intensity) 
    { 
        duration = 0; 
    }

    public override void UpdateEffect()
    {
        if (duration <= 0)
        {
            active = false;
        }
        if (!active) return;
        if (timer == null) timer = new Timer(tickRate, UpdateEffect); else timer.Reset(tickRate);
        duration -= tickRate;
        owner.Damage(damage);
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        duration += intensity / 50f;
    }
}

public class IceStatusEffect : BaseStatusEffect
{
    private float damage;
    public IceStatusEffect(int intensity) : base(intensity) 
    {
        duration = 3f; 
    }

    public override void UpdateEffect()
    {
        if (timer == null) timer = new Timer(duration, BurstDamage);
        if (timer.timeLeft <= 0)
        {
            timer.Reset(duration);
            damage = 0;
        }
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        damage += intensity / 5f;
    }

    private void BurstDamage()
    {
        owner.Damage(damage);
    }
}
