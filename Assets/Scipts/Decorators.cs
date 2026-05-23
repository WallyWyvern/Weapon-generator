using UnityEngine;

public abstract class WeaponDecorator
{
    public int damage { get; set; }
    public DamageType damageType { get; set; }
    public WeaponDecorator(int _damage)
    {
        damage = _damage;

    }

    public abstract IWeapon Decorate(IWeapon weapon);
}


public class FireDecorator : WeaponDecorator
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
