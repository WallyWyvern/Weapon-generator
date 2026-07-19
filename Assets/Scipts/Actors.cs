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
    protected bool isDead = false;

    public BaseActor(GameObject go, float speed, Vector3 itemPos, float health, Vector3 initPos)
    {
        gameObject = GameObject.Instantiate(go);
        moveSpeed = speed;
        heldItemPos = itemPos;
        this.health = health;
        gameObject.transform.position = initPos;
        EventManager.instance.onBulletCollision += HandleCollision;
        activeEffects = new List<IEffect>();
        SetupUI();
    }

    public abstract void Move(Vector3 moveVector);

    public abstract void SetHeldObject(IUsable item);

    public virtual void Damage(float damage)
    {
        health -= damage;
        UpdateHealthUI();

        if (health <= 0)
        { 
            gameObject.SetActive(false);
            // call pool stuff here if I do that
        }
    }

    public virtual void HandleEffects()
    {
        for (int i = 0; i < activeEffects.Count - 1; i++)
        {
            activeEffects[i].nextHandler = activeEffects[i + 1];
        }
        for (int i = 0; i < activeEffects.Count; i++)
        {
            activeEffects[i].active = true;
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

        foreach (IEffect effect in effects)
        {
            activeEffects.Add(effect);
        }
        HandleEffects();
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
    private int immunityChance;

    public Enemy(GameObject go, float speed, Vector3 itemPos, float health, Vector3 initPos, int immunityChance) : base(go, speed, itemPos, health, initPos)
    {
        collider = gameObject.AddComponent<BoxCollider>();
        this.immunityChance = immunityChance;
        SetImmunity();

        EventManager.instance.onPlayerMoved += SetMoveVector;
        EventManager.instance.onSendUpdateTick += this.CheckCollision;
    }

    public override void Move(Vector3 moveVector)
    {
        gameObject.transform.position += moveVector * moveSpeed * Time.fixedDeltaTime;
    }

    public override void SetHeldObject(IUsable item)
    {
        
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (health <= 0)
        {
            EventManager.instance.onPlayerMoved -= SetMoveVector;
            EventManager.instance.onSendUpdateTick -= this.CheckCollision;
            if (!isDead) EventManager.instance.EnemyDeath();
            isDead = true; // replace with isactive from objectpool
        }
    }

    private void SetImmunity()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (Random.Range(0,100) <= immunityChance)
        {
            activeEffects.Add(new FireImmunityEffect());
            spriteRenderer.color = Color.orange;
            return;
        }

        if (Random.Range(0, 100) <= immunityChance)
        {
            activeEffects.Add(new PoisonImmunityEffect());
            spriteRenderer.color = Color.green;
            return;
        }

        if (Random.Range(0, 100) <= immunityChance)
        {
            activeEffects.Add(new IceImmunityEffect());
            spriteRenderer.color = Color.lightBlue;
            return;
        }
    }

    private void SetMoveVector(Vector3 pos) 
    {
        var aimDirection = pos - gameObject.transform.position;
        aimDirection.Normalize();
        Move(aimDirection);
    }

    public void CheckCollision()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(0.5f, 0.5f, 0));
        foreach (Collider collider in hitColliders)
        {
            EventManager.instance.OnEnemyCollision(collider);
        }
    }
}