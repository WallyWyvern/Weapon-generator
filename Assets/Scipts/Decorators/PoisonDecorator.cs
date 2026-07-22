public class PoisonDecorator : BaseWeaponDecorator
{
    public override IWeapon Decorate(IWeapon weapon, int intensity)
    {
        // strictly for UI, very crude fix because of time crunch
        EventManager.instance.WeaponDecorated(EffectType.poison);

        PoisonStatusEffect poisonEffect = new PoisonStatusEffect(intensity);
        weapon.weaponEffects.Add(poisonEffect);
        return weapon;
    }
}
