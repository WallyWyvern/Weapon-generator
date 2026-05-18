using UnityEngine;

public abstract class WeaponDamageDecorator
{
    public int damage { get; set; }
    public DamageType damageType { get; set; }
    public WeaponDamageDecorator(int _damage)
    {
        damage = _damage;

    }

    public abstract IWeapon Decorate(IWeapon weapon);
}


public class FireDecorator : WeaponDamageDecorator
{
    // tick speed, duration

    public FireDecorator(int _damage) : base(_damage) 
    {
        damageType = DamageType.Fire;
    }

    public override IWeapon Decorate(IWeapon weapon)
    {
        weapon.DamageTypes.Add(damageType, damage);
        return weapon;
    }
}
