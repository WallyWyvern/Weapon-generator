public class PoisonStatusEffect : BaseStatusEffect
{
    private float tickRate = 0.5f;
    private float damage = 5f;
    public PoisonStatusEffect(int intensity) : base(intensity) 
    { 
        duration = 0; 
    }

    public override void UpdateEffect()
    {
        if (duration <= 0)
        {
            active = false;
        }
        if (!active) return;
        if (timer == null) timer = new Timer(tickRate, UpdateEffect); else timer.Reset(tickRate);
        duration -= tickRate;
        owner.Damage(damage);
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        duration += intensity / 50f;
    }
}
