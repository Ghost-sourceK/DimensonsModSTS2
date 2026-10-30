using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicEntropy : DimensionsModCardModel
{
    public CosmicEntropy() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithVar("PlayPurge", 3, 1);
        WithVar("dazed", 2);
        WithKeyword(CardKeyword.Exhaust);
        WithDimensionTags(DimensionType.Cosmic);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ExhaustPlaySystem.PlayFromExhaustPile(choiceContext, Owner, DynamicVars["PlayPurge"].IntValue, null, true);
        
        await CreateCardCmd.GiveCards<Dazed>(Owner, DynamicVars["dazed"].IntValue, PileType.Exhaust, animationTime: 0.1f);
    }
}
