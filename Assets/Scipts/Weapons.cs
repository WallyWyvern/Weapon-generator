using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRangedWeapon : IRangedWeapon, IGameObject
{
    public int magSize { get; set; }
    public float reloadSpeed { get; set; }
    public float attackSpeed { get; set; }
    public float projectileSpeed { get; set; }
    public List<IEffect> weaponEffects { get; set; }
    public GameObject gameObject { get; set; }

    public void Use()
    {
        
    }
}

public abstract class BaseProjectile : IProjectile, IGameObject, IPoolable
{
    public List<IEffect> effects { get; set; }
    public float projectileSpeed { get; set; }
    public GameObject gameObject { get; set; }
    public bool active { get; set; }

    BaseProjectile(GameObject go, float speed) 
    {
        this.gameObject = go;
        this.projectileSpeed = speed;
    }

    public void Move(Vector3 dir)
    {
        if (!active) return;
        gameObject.transform.position += dir * projectileSpeed;
    }

    public void OnEnableObject()
    {
        throw new System.NotImplementedException();
    }

    public void OnDissableObject()
    {
        throw new System.NotImplementedException();
    }
}













public class TestWeapon : IWeapon
{

    public List<IEffect> weaponEffects { get; set; }

    public TestWeapon()
    {
        weaponEffects = new List<IEffect>();
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

    // future fire mechanics n stuff
    // object pool stuff
}

public class TestEnemy : IStatusEffectable, IDamageable
{

    public List<IEffect> activeEffects { get; set; }

    public TestEnemy() { }

    public void Damage(float damage)
    {
        
    }

    public void HandleEffects()
    {
        
    }
}