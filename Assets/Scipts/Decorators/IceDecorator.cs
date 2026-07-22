public class IceDecorator : BaseWeaponDecorator
{
    public override IWeapon Decorate(IWeapon weapon, int intensity)
    {
        // strictly for UI, very crude fix because of time crunch
        EventManager.instance.WeaponDecorated(EffectType.ice);

        IceStatusEffect iceEffect = new IceStatusEffect(intensity);
        weapon.weaponEffects.Add(iceEffect);
        return weapon;
    }
}
