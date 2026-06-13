using UnityEngine;

public abstract class BaseWeaponDecorator
{
    public BaseWeaponDecorator()
    {
    }

    public abstract IWeapon Decorate(IWeapon weapon, int intensity);
}

public class RangedWeaponDecorator : BaseWeaponDecorator
{
    public RangedWeaponDecorator() : base() { }

    public override IWeapon Decorate(IWeapon weapon, int intensity)
    {
        // not required here
        return weapon;
    }

    public IRangedWeapon DecorateRanged(IRangedWeapon weapon, int intensity)
    {
        // intensity range from 25 - 100
        RegularDamage dmg = new RegularDamage(intensity);
        weapon.weaponEffects.Add(dmg);

        weapon.attackCooldown = 10f / intensity;
        weapon.reloadTime = 50f / intensity;
        weapon.magSize = intensity / 5;
        weapon.projectileLifeTime = intensity / 25f;
        weapon.projectileSpeed = intensity / 5f;

        return weapon;
    }
}


public class FireDecorator : BaseWeaponDecorator
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
