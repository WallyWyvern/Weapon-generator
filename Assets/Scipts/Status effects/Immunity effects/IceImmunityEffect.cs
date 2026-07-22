using System.Collections.Generic;

public class IceImmunityEffect : BaseImmunityEffect
{
    public override void Handle(List<IEffect> activeEffects, IStatusEffectable owner)
    {
        foreach (var effect in activeEffects)
        {
            if (effect is IceStatusEffect)
            {
                foundEffects.Add(effect);
            }
        }
        base.Handle(activeEffects, owner);
    }
}