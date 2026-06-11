using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRangedWeapon : IRangedWeapon
{
    public int magSize { get; set; }
    public float reloadSpeed { get; set; }
    public float attackSpeed { get; set; }
    public float projectileSpeed { get; set; }
    public float projectileLifeTime { get; set; }
    public List<IEffect> weaponEffects { get; set; }
    public GameObject gameObject { get; set; }
    public GameObject projectileObject { get; set; }

    protected Player owner;

    public BaseRangedWeapon(GameObject go, GameObject projectileGo, Player _owner, Vector3 offset)
    {
        owner = _owner;
        gameObject = GameObject.Instantiate(go, owner.gameObject.transform);
        gameObject.transform.localPosition = offset;
        this.projectileObject = projectileGo;
    }

    public abstract void Use();
}


public class Gun : BaseRangedWeapon 
{
    private ObjectPool<Bullet> bulletPool = new ObjectPool<Bullet>();
    public Gun(GameObject go, GameObject projectileGo, Player owner, Vector3 offset) : base(go, projectileGo, owner, offset) 
    {
        // debug
        projectileLifeTime = 2f;
        projectileSpeed = 10f;
        //gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.SetActive(true);
    }

    public override void Use()
    {
        //owner.gameObject.transform.up = aimDirection;
        var tempBullet = bulletPool.RequestObject();
        tempBullet.onBulletTimerFinished += HandleProjectileFinished;
        tempBullet.Setup(projectileObject, 
            owner.aimDirection,
            projectileSpeed, 
            projectileLifeTime, 
            weaponEffects, 
            gameObject.transform.position);
    }

    private void HandleProjectileFinished(Bullet projectile)
    {
        projectile.onBulletTimerFinished -= HandleProjectileFinished;
        bulletPool.ReturnObjectToPool(projectile);
    }
}