using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : IWeapon
{

    public List<IEffect> weaponEffects { get; set; }

    public TestWeapon()
    {
        weaponEffects = new List<IEffect>();
    }

    // future fire mechanics n stuff
    // object pool stuff
}

public class TestEnemy : IStatusEffectable, IDamageable
{

    public List<IEffect> activeEffects { get; set; }

    public TestEnemy() { }

    public void Damage(float damage)
    {
        
    }

    public void HandleEffects()
    {
        
    }
}