using System;
using UnityEngine;

public class Bullet : BaseProjectile
{
    public event Action<Bullet> onDespawnBullet;

    public Bullet() : base() { }

    protected override void OnProjectileTimerFinished()
    {
         onDespawnBullet?.Invoke(this);
    }

    public override void CheckCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.1f);
        foreach (Collider collider in hitColliders)
        {
            EventManager.instance.OnBulletCollision(collider, effects);
            onDespawnBullet?.Invoke(this);
        }
    }
}