using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EffectType
{
    Fire,
    Ice,
    Poison,
    Arcane,
    Normal
}


public interface IPoolable
{
    bool active { get; set; }
    void OnEnableObject();
    void OnDissableObject();
}

public interface IWeapon
{
    // weapon stuff
    List<IEffect> weaponEffects { get; set; }
}

public interface IRangedWeapon : IWeapon
{

}

public interface IMeleeWeapon : IWeapon
{

}

public interface IStatusEffectable 
{
    List<IEffect> activeEffects { get; set; }
    void HandleEffects();
}

public interface IDamageable
{
    void Damage(float damage);
}
