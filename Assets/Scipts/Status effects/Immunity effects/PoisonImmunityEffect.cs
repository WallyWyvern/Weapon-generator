using System.Collections.Generic;

public class PoisonImmunityEffect : BaseImmunityEffect
{
    public override void Handle(List<IEffect> activeEffects, IStatusEffectable owner)
    {
        foreach (var effect in activeEffects)
        {
            if (effect is PoisonStatusEffect)
            {
                foundEffects.Add(effect);
            }
        }
        base.Handle(activeEffects, owner);
    }
}
