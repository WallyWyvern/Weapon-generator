using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IPoolable
{
    bool active { get; set; }
    void OnEnableObject();
    void OnDissableObject();
}

public interface IUsable : IGameObject
{
    void Use();
}

public interface IWeapon : IUsable
{
    // weapon stuff
    List<IEffect> weaponEffects { get; set; }
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
    void CheckCollision();
}

public interface IStatusEffectable : IDamageable
{
    List<IEffect> activeEffects { get; set; }
    void HandleEffects();
    void HandleCollision(Collider _collider, List<IEffect> effects);
}

public interface IDamageable
{
    float health { get; set; }
    void Damage(float damage);
}

public interface IGameObject
{
    GameObject gameObject { get; set; }
}