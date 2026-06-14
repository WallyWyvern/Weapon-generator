using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static EventManager instance;

    public EventManager() { instance = this; }

    public event Action onSendUpdateTick;
    public void SendUpdateTick() { if (onSendUpdateTick != null) { onSendUpdateTick(); } }

    public event Action<float> onTickTimers;
    public void TickTimers(float delta) { if (onTickTimers != null) { onTickTimers(delta); } }

    public event Action<Collider, List<IEffect>> onCollision;
    public void OnCollision(Collider collider, List<IEffect> effectList) { if (onCollision != null) { onCollision(collider, effectList); } }

    public event Action<EffectType> onWeaponDecorated;
    public void WeaponDecorated(EffectType type) { if (onWeaponDecorated != null) { onWeaponDecorated(type); } }
}