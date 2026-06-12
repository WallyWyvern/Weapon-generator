using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : IProjectile, IGameObject, IPoolable
{
    public List<IEffect> effects { get; set; }
    public float projectileSpeed { get; set; }
    public GameObject gameObject { get; set; }
    public float lifeTime { get; set; }
    public Vector3 direction { get; set; }
    public bool active { get; set; }
    protected Timer timer { get; set; }

    protected BaseProjectile()
    {

    }

    public virtual void Setup(GameObject go, Vector3 dir, float speed, float lifeTime, List<IEffect> effects, Vector3 startPosition)
    {
        if (gameObject == null)
        {
            this.gameObject = GameObject.Instantiate(go);
        }
        this.direction = dir;
        this.projectileSpeed = speed;
        this.effects = effects;
        gameObject.transform.position = startPosition;
        if (timer == null)
        {
            timer = new Timer(lifeTime, OnProjectileTimerFinished);
        }
        else { timer.Reset(lifeTime); }

    }

    public virtual void Move() // trigger on update event
    {
        if (!active) return;
        gameObject.transform.position += direction.normalized * projectileSpeed * Time.fixedDeltaTime;
    }

    public virtual void OnEnableObject()
    {
        gameObject?.SetActive(true);
        EventManager.instance.onSendUpdateTick += this.Move;
        EventManager.instance.onSendUpdateTick += this.CheckCollision;
    }

    public virtual void OnDissableObject()
    {
        gameObject?.SetActive(false);
        EventManager.instance.onSendUpdateTick -= this.Move;
        EventManager.instance.onSendUpdateTick -= this.CheckCollision;
    }

    protected abstract void OnProjectileTimerFinished();

    public virtual void CheckCollision()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.5f);
        foreach (Collider collider in hitColliders)
        {
            EventManager.instance.OnCollision(collider, effects);
        }
    }
}


public class Bullet : BaseProjectile
{
    public event Action<Bullet> onDespawnBullet;

    public Bullet() : base()
    {
       // Debug.Log("Bullet constructor triggered");

    }

    protected override void OnProjectileTimerFinished()
    {
         onDespawnBullet?.Invoke(this);
    }
    public override void CheckCollision()
    {

        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.1f);
        foreach (Collider collider in hitColliders)
        {
            EventManager.instance.OnCollision(collider, effects);
            onDespawnBullet?.Invoke(this);
        }
    }
}