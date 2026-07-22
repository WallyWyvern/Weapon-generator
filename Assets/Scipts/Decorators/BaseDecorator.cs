using UnityEngine;

public abstract class BaseWeaponDecorator
{
    public abstract IWeapon Decorate(IWeapon weapon, int intensity);
}
