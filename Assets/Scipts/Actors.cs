using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActor : IGameObject, IStatusEffectable
{
    public GameObject gameObject { get; set; }
    public IUsable heldItem { get; set; }
    public Vector3 heldItemPos { get; set; }
    public float moveSpeed { get; set; }
    public float health { get; set; }
    public List<IEffect> activeEffects { get; set; }

    protected Collider collider { get; set; }

    public BaseActor(GameObject go, float speed, Vector3 itemPos, float health, Vector3 initPos)
    {
        gameObject = GameObject.Instantiate(go);
        moveSpeed = speed;
        heldItemPos = itemPos;
        this.health = health;
        gameObject.transform.position = initPos;
        collider = gameObject.AddComponent<BoxCollider>();
        EventManager.instance.onCollision += HandleCollision;
    }
    public abstract void Move(Vector3 moveVector);
    public abstract void SetHeldObject(IUsable item);

    public virtual void Damage(float damage)
    {
        health -= damage;
    }

    public void HandleEffects()
    {
        for (int i = 0; i < activeEffects.Count - 1; i++)
        {
            activeEffects[i].NextHandler = activeEffects[i + 1];
        }
        if (activeEffects.Count > 0)
        {
            activeEffects[0]?.Handle(activeEffects, this);
        }
    }

    public void HandleCollision(Collider _collider, List<IEffect> effects)
    {
        if (collider == null)
        {
            Debug.Log("Actor collider not found");
           return;
        }
        if (_collider != collider) return;
        activeEffects = effects;
        HandleEffects();
        Debug.Log("Actor was hit!");
    }
}


public class Enemy : BaseActor
{


    public Enemy(GameObject go, float speed, Vector3 itemPos, float health, Vector3 initPos) : base(go, speed, itemPos, health, initPos)
    {
    }

    public override void Move(Vector3 moveVector)
    {
        
    }

    public override void SetHeldObject(IUsable item)
    {
        
    }
}