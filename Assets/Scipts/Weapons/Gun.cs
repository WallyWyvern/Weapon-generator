using UnityEngine;

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
            if (reloadTimer == null)
            {
                reloadTimer = new Timer(reloadTime, Reload);
            }
            else 
            { 
                reloadTimer.Reset(reloadTime); 
            }
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
        if (attackTimer == null) 
        { 
            attackTimer = new Timer(attackCooldown, ResetCooldown); 
        } else 
        { 
            attackTimer.Reset(attackCooldown); 
        }
        onCooldown = true;
    }

    private void HandleProjectileFinished(Bullet projectile)
    {
        if (!projectile.active) return;
        projectile.onDespawnBullet -= HandleProjectileFinished;
        bulletPool.ReturnObjectToPool(projectile);
    }
}