using UnityEngine;

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