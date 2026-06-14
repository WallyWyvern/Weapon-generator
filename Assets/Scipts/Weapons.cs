using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRangedWeapon : IRangedWeapon
{
    public int magSize { get; set; }
    public float reloadTime { get; set; }
    public float attackCooldown { get; set; }
    public float projectileSpeed { get; set; }
    public float projectileLifeTime { get; set; }
    public List<IEffect> weaponEffects { get; set; }
    public GameObject gameObject { get; set; }
    public GameObject projectileObject { get; set; }

    protected Player owner;
    protected bool onCooldown;
    protected int currentAmmo;

    protected Timer attackTimer;
    protected Timer reloadTimer;

    public BaseRangedWeapon(GameObject go, GameObject projectileGo, Player _owner)
    {
        owner = _owner;
        gameObject = GameObject.Instantiate(go, owner.gameObject.transform);
        this.projectileObject = projectileGo;
        weaponEffects = new List<IEffect>();
        onCooldown = false;
    }

    public abstract void Use();

    public virtual void Reload()
    {
        currentAmmo = magSize;
        onCooldown = false;
    }

    public void ResetCooldown() { onCooldown = false; }
}


public class Gun : BaseRangedWeapon 
{
    private ObjectPool<Bullet> bulletPool = new ObjectPool<Bullet>();

    public Gun(GameObject go, GameObject projectileGo, Player owner) : base(go, projectileGo, owner) 
    {
        gameObject.SetActive(true);
    }

    public override void Use()
    {
        if (onCooldown) return;
        if (currentAmmo <= 0) 
        {
            if (reloadTimer == null) reloadTimer = new Timer(reloadTime, Reload); else reloadTimer.Reset(reloadTime);
            onCooldown = true;
            return;
        }
        var tempBullet = bulletPool.RequestObject();
        tempBullet.onDespawnBullet += HandleProjectileFinished;
        tempBullet.Setup(projectileObject, 
            owner.aimDirection,
            projectileSpeed, 
            projectileLifeTime, 
            weaponEffects, 
            gameObject.transform.position);
        currentAmmo--;
        if (attackTimer == null) attackTimer = new Timer(attackCooldown, ResetCooldown); else attackTimer.Reset(attackCooldown);
        onCooldown = true;
    }

    private void HandleProjectileFinished(Bullet projectile)
    {
        if (!projectile.active) return;
        projectile.onDespawnBullet -= HandleProjectileFinished;
        bulletPool.ReturnObjectToPool(projectile);
    }
}