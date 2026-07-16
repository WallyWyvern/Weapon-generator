using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static EventManager instance;

    public EventManager() { instance = this; }

    // update ticks
    public event Action onSendUpdateTick;
    public void SendUpdateTick() { if (onSendUpdateTick != null) { onSendUpdateTick(); } }

    public event Action<float> onTickTimers;
    public void TickTimers(float delta) { if (onTickTimers != null) { onTickTimers(delta); } }

    public event Action<Vector3> onPlayerMoved;
    public void PlayerMoved(Vector3 newPos) { if (onPlayerMoved != null) { onPlayerMoved(newPos); } }

    // weapon events
    public event Action<Collider, List<IEffect>> onCollision;
    public void OnCollision(Collider collider, List<IEffect> effectList) { if (onCollision != null) { onCollision(collider, effectList); } }

    public event Action<EffectType> onWeaponDecorated;
    public void WeaponDecorated(EffectType type) { if (onWeaponDecorated != null) { onWeaponDecorated(type); } }

    // gameloop events
    public event Action onEnemyDeath;
    public void EnemyDeath() { if (onEnemyDeath != null) { onEnemyDeath(); } }

    public event Action onPlayerDeath;
    public void PlayerDeath() { if (onPlayerDeath != null) { onPlayerDeath(); } }
}