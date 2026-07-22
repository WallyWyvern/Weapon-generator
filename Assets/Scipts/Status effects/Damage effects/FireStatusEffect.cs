public class FireStatusEffect : BaseStatusEffect
{
    private float tickRate;
    private float damage;

    public FireStatusEffect(int intensity) : base(intensity) { }

    public override void UpdateEffect()
    {
        if (duration <= 0)
        {
            active = false;
        } 
        if (!active) return;
        if (timer == null) timer = new Timer(tickRate, UpdateEffect); else timer.Reset(tickRate);
        owner.Damage(damage);
        duration -= tickRate;
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        tickRate = 20f / intensity;
        duration = intensity * 0.05f;
        damage = intensity * 0.1f;
    }
}
