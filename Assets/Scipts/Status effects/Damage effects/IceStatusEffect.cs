public class IceStatusEffect : BaseStatusEffect
{
    private float damage;
    public IceStatusEffect(int intensity) : base(intensity) 
    {
        duration = 3f; 
    }

    public override void UpdateEffect()
    {
        if (!active) return;
        if (timer == null) timer = new Timer(duration, BurstDamage);
        if (timer.timeLeft <= 0)
        {
            timer.Reset(duration);
            damage = 0;
        }
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        damage += intensity / 5f;
    }

    private void BurstDamage()
    {
        owner.Damage(damage);
    }
}
