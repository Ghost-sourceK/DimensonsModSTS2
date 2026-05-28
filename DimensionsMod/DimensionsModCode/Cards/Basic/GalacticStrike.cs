using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Abstract;

namespace DimensionsMod.DimensionsModCode.Cards.Basic;

[Pool(typeof(DimensionsModCardPool))]
public sealed class GalacticStrike : DimensionsModCardModel
{
    public GalacticStrike() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        WithDamage(6, 3);
        WithTags(CardTag.Strike);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }
}
