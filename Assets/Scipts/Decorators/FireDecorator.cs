public class FireDecorator : BaseWeaponDecorator
{
    public override IWeapon Decorate(IWeapon weapon, int intensity)
    {
        // strictly for UI, very crude fix because of time crunch
        EventManager.instance.WeaponDecorated(EffectType.fire);

        FireStatusEffect fireEffect = new FireStatusEffect(intensity);
        weapon.weaponEffects.Add(fireEffect);
        return weapon;
    }
}
