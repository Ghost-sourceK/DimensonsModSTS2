using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicExoplanet : DimensionsModCardModel
{
    public CosmicExoplanet() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(7, 3);
        WithCards(1);
        WithDimensionTags(DimensionType.Cosmic);
        WithTip(ExhumeKeyWord.Exhume);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        await ExhumeSystem.ExhumeToHand(choiceContext, Owner, 1, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"));
    }
}
