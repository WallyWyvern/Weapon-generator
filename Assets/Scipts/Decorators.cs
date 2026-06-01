using UnityEngine;

public abstract class WeaponDecorator
{
    public WeaponDecorator()
    {
    }

    public abstract IWeapon Decorate(IWeapon weapon, int intensity);
}


public class FireDecorator : WeaponDecorator
{
    public FireDecorator() : base() 
    {
    }

    public override IWeapon Decorate(IWeapon weapon, int intensity)
    {
        FireStatusEffect fireEffect = new FireStatusEffect(intensity);
        weapon.weaponEffects.Add(fireEffect);
        return weapon;
    }
}
