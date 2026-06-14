using UnityEngine;

public abstract class BaseWeaponDecorator
{
    public abstract IWeapon Decorate(IWeapon weapon, int intensity);
}

public class RangedWeaponDecorator : BaseWeaponDecorator
{
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
    public override IWeapon Decorate(IWeapon weapon, int intensity)
    {
        // strictly for UI, very crude fix because of time crunch
        EventManager.instance.WeaponDecorated(EffectType.fire);

        FireStatusEffect fireEffect = new FireStatusEffect(intensity);
        weapon.weaponEffects.Add(fireEffect);
        return weapon;
    }
}

public class PoisonDecorator : BaseWeaponDecorator
{
    public override IWeapon Decorate(IWeapon weapon, int intensity)
    {
        // strictly for UI, very crude fix because of time crunch
        EventManager.instance.WeaponDecorated(EffectType.poison);

        PoisonStatusEffect poisonEffect = new PoisonStatusEffect(intensity);
        weapon.weaponEffects.Add(poisonEffect);
        return weapon;
    }
}

public class IceDecorator : BaseWeaponDecorator
{
    public override IWeapon Decorate(IWeapon weapon, int intensity)
    {
        // strictly for UI, very crude fix because of time crunch
        EventManager.instance.WeaponDecorated(EffectType.ice);

        IceStatusEffect iceEffect = new IceStatusEffect(intensity);
        weapon.weaponEffects.Add(iceEffect);
        return weapon;
    }
}
