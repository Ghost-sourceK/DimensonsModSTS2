using BaseLib.Extensions;
using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaStrike : DimensionsModCardModel
{
    public PlasmaStrike() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy)
    {
        WithDamage(3);
        WithVars(new RepeatVar(4).WithUpgrade(1));
        WithDimensionTags(DimensionType.Plasma);
        WithTags(CardTag.Strike);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitCount(DynamicVars.Repeat.IntValue).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
    }
}
