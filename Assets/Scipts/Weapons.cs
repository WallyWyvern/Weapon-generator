using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRangedWeapon : IRangedWeapon, IGameObject
{
    public int magSize { get; set; }
    public float reloadSpeed { get; set; }
    public float attackSpeed { get; set; }
    public float projectileSpeed { get; set; }
    public float projectileLifeTime { get; set; }
    public List<IEffect> weaponEffects { get; set; }
    public GameObject gameObject { get; set; }
    public GameObject projectileObject { get; set; }

    public BaseRangedWeapon(GameObject go, GameObject projectileGo)
    {
        gameObject = go;
        this.projectileObject = projectileGo;
    }

    public abstract void Use();
}


public class Gun : BaseRangedWeapon 
{
    private ObjectPool<Bullet> bulletPool = new ObjectPool<Bullet>();
    public Gun(GameObject go, GameObject projectileGo) : base(go, projectileGo) 
    {
        // debug
        projectileLifeTime = 2f;
        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.SetActive(true);
    }

    public override void Use()
    {
        var tempBullet = bulletPool.RequestObject();
        tempBullet.onBulletTimerFinished += HandleProjectileFinished;
        tempBullet.Setup(projectileObject, new Vector3(1, 1, 0), 0.1f, projectileLifeTime, weaponEffects, gameObject.transform.position);

        int x = 10;
    }

    private void HandleProjectileFinished(Bullet projectile)
    {
        bulletPool.ReturnObjectToPool(projectile);
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