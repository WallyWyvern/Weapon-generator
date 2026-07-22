using System.Collections.Generic;

public class FireImmunityEffect : BaseImmunityEffect
{
    public override void Handle(List<IEffect> activeEffects, IStatusEffectable owner)
    {
        foreach (var effect in activeEffects)
        {
            if (effect is FireStatusEffect)
            {
                foundEffects.Add(effect);
            }
        }
        base.Handle(activeEffects, owner);
    }
}
