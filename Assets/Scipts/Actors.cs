 using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseActor : IGameObject, IStatusEffectable
{
    public GameObject gameObject { get; set; }
    public GameObject textObject { get; set; }
    public TextMeshPro healthUI { get; set; }
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
        SetupUI();
    }
    public abstract void Move(Vector3 moveVector);
    public abstract void SetHeldObject(IUsable item);

    public virtual void Damage(float damage)
    {
        health -= damage;
        UpdateHealthUI();
    }

    public virtual void HandleEffects()
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

    public virtual void HandleCollision(Collider _collider, List<IEffect> effects)
    {
        if (collider == null)
        {
            Debug.Log("Actor collider not found");
           return;
        }
        if (_collider != collider) return;
        activeEffects = effects;
        HandleEffects();
        // debug
        Debug.Log("Actor was hit!");
    }

    public virtual void UpdateHealthUI()
    { 
        healthUI?.SetText(health.ToString());
    }

    public virtual void SetupUI()
    {
        textObject = new GameObject();
        healthUI = textObject.AddComponent<TextMeshPro>();
        textObject.transform.parent = gameObject.transform;
        textObject.transform.localPosition = new Vector3(0, 0.75f, 0);

        healthUI.fontSize = 5;
        healthUI.alignment = TextAlignmentOptions.Center;
        UpdateHealthUI();
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