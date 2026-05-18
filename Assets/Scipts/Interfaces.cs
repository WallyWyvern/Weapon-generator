using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Fire,
    Ice,
    Poison,
    Arcane
}


public interface IWeapon
{
    // weapon stuff
    Dictionary<DamageType, int> DamageTypes { get; set; }
}

public interface IRangedWeapon : IWeapon
{

}

public interface IMeleeWeapon : IWeapon
{

}
