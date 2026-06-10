using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IPoolable
{
    bool active { get; set; }
    void OnEnableObject();
    void OnDissableObject();
}

public interface IWeapon : IGameObject
{
    // weapon stuff
    List<IEffect> weaponEffects { get; set; }
    void Use();
}

public interface IRangedWeapon : IWeapon
{
    int magSize { get; set; }
    float reloadSpeed { get; set; }
    float attackSpeed { get; set; }
    float projectileSpeed { get; set; }
    float projectileLifeTime { get; set; }

}

public interface IProjectile
{
    List<IEffect> effects { get; set; }
    float projectileSpeed { get; set; }
    void Move();
}

public interface IStatusEffectable : IDamageable
{
    List<IEffect> activeEffects { get; set; }
    void HandleEffects();
}

public interface IDamageable
{
    void Damage(float damage);
}

public interface IGameObject
{
    GameObject gameObject { get; set; }
}