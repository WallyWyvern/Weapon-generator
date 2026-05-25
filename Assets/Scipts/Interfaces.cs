using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Fire,
    Ice,
    Poison,
    Arcane,
    Normal
}


public interface IWeapon
{
    // weapon stuff
    Dictionary<EffectType, int> weaponEffects { get; set; }
}

public interface IRangedWeapon : IWeapon
{

}

public interface IMeleeWeapon : IWeapon
{

}

public interface IDamageable 
{
    List<IEffect> activeEffects { get; set; }
    void HandleEffects();
}
