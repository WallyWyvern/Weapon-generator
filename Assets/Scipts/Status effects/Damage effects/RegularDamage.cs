public class RegularDamage : BaseStatusEffect
{
    public float damage;

    public RegularDamage(int intensity) : base(intensity) { }

    public override void UpdateEffect()
    {
        owner.Damage(damage);
    }

    protected override void CalculateStatusEffect(int intensity)
    {
        damage = intensity / 10f;
    }
}
