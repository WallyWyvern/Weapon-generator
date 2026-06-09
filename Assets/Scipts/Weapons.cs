using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRangedWeapon : IRangedWeapon, IGameObject
{
    public int magSize { get; set; }
    public float reloadSpeed { get; set; }
    public float attackSpeed { get; set; }
    public float projectileSpeed { get; set; }
    public float projectileLifeTime { get; set; }
    public List<IEffect> weaponEffects { get; set; }
    public GameObject gameObject { get; set; }
    public GameObject projectileObject { get; set; }

    public BaseRangedWeapon(GameObject go, GameObject projectileGo)
    {
        gameObject = go;
        this.projectileObject = projectileGo;
    }

    public abstract void Use();
}


public class Gun : BaseRangedWeapon 
{
    private ObjectPool<Bullet> bulletPool = new ObjectPool<Bullet>();
    public Gun(GameObject go, GameObject projectileGo) : base(go, projectileGo) 
    {
        // debug
        projectileLifeTime = 2;
        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.SetActive(true);
    }

    public override void Use()
    {
        var tempBullet = bulletPool.RequestObject();
        tempBullet.Setup(projectileObject, new Vector3(1, 1, 0), 0.1f, weaponEffects, gameObject.transform.position);
        // ask about invoking without monobehaviour
    }
}

public abstract class BaseProjectile : IProjectile, IGameObject, IPoolable
{
    public List<IEffect> effects { get; set; }
    public float projectileSpeed { get; set; }
    public GameObject gameObject { get; set; }
    public float lifeTime { get; set; }
    public Vector3 direction { get; set; }
    public bool active { get; set; }

    protected BaseProjectile() 
    {
        
    }

    public virtual void Setup(GameObject go, Vector3 dir, float speed, List<IEffect> effects, Vector3 startPosition) 
    {
        this.gameObject = go;
        this.direction = dir;
        this.projectileSpeed = speed;
        this.effects = effects;
        gameObject.transform.position = startPosition;
    }

    public virtual void Move() // trigger on update event
    {
        if (!active) return;
        gameObject.transform.position += direction * projectileSpeed;
    }

    public virtual void OnEnableObject()
    {
        gameObject?.SetActive(true);
        EventManager.instance.onSendUpdateTick += this.Move; // why does instance of bullet get triggered by this?
    }

    public virtual void OnDissableObject()
    {
        gameObject?.SetActive(false);
        EventManager.instance.onSendUpdateTick -= this.Move;
    }
}


public class Bullet : BaseProjectile
{
    public Bullet() { //Debug.Log("Bullet constructor triggered");
        
    }

}













public class TestWeapon : IWeapon
{

    public List<IEffect> weaponEffects { get; set; }

    public TestWeapon()
    {
        weaponEffects = new List<IEffect>();
    }

    public void Use()
    {
        throw new System.NotImplementedException();
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