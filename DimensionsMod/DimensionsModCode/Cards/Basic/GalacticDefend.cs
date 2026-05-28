using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Abstract;

namespace DimensionsMod.DimensionsModCode.Cards.Basic;

[Pool(typeof(DimensionsModCardPool))]
public sealed class GalacticDefend : DimensionsModCardModel
{
    public GalacticDefend() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        WithBlock(5, 3);
        WithTags(CardTag.Defend);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
    }
}
