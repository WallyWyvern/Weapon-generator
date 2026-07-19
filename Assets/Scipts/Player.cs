using System.Collections.Generic;
using UnityEngine;

public class Player : BaseActor
{
    public Vector3 aimDirection;

    public Player(GameObject go, float speed, Vector3 itemPos, float health, Vector3 initPos) : base(go, speed, itemPos, health, initPos) 
    {
        collider = gameObject.AddComponent<SphereCollider>();
        EventManager.instance.onEnemyCollision += HandleCollision;
    }

    public override void Move(Vector3 moveVector)
    {
        gameObject.transform.position += moveVector.normalized * moveSpeed;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        aimDirection = mousePosition - gameObject.transform.position;
        aimDirection.Normalize();
        gameObject.transform.up = aimDirection;
        EventManager.instance.PlayerMoved(gameObject.transform.position);
    }

    public override void SetHeldObject(IUsable item)
    {
        heldItem = item;
        heldItem.gameObject.transform.localPosition = heldItemPos;
    }

    public override void SetupUI()
    {
       // does nothing because I dont want player health right now
    }

    // Not using base as it would complicate things and id like to just get this done,
    // if I would have done this properly I would have used the effect system to add damage to player and override base method instead of brute forcing the damage.
    public void HandleCollision(Collider _collider)
    {
        if (collider == null)
        {
            Debug.Log("Actor collider not found");
            return;
        }
        if (_collider != collider) return;
        Damage(100);
    }
    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (health <= 0)
        {
            EventManager.instance.onEnemyCollision -= HandleCollision;
            if (!isDead) EventManager.instance.PlayerDeath();
            isDead = true; // replace with isactive from objectpool
        }
    }
}
