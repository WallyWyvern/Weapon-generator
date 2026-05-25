using UnityEngine;

public abstract class WeaponDecorator
{
    public int intensity { get; set; }
    public EffectType effectType { get; set; }
    public WeaponDecorator(int _intensity)
    {
        intensity = _intensity;

    }

    public abstract IWeapon Decorate(IWeapon weapon);
}


public class FireDecorator : WeaponDecorator
{
    // tick speed, duration

    public FireDecorator(int _damage) : base(_damage) 
    {
        effectType = EffectType.Fire;
    }

    public override IWeapon Decorate(IWeapon weapon)
    {
        weapon.weaponEffects.Add(effectType, intensity);
        return weapon;
    }
}
