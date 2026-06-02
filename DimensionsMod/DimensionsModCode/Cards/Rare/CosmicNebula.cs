using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace DimensionsMod.DimensionsModCode.Cards.Rare;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicNebula : DimensionsModCardModel
{
    public CosmicNebula() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithCards(3, 1);
        WithDimensionTags(DimensionType.Cosmic);
        WithKeywords(CardKeyword.Retain, CardKeyword.Exhaust);
        WithEnergyTip();
        WithTip(ExhumeKeyWord.Exhume);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ExhumeSystem.ExhumeToHand
            (choiceContext, Owner, DynamicVars.Cards.IntValue, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"), reduceCost: true);
    }
}