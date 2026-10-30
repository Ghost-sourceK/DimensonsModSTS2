using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicBlink : DimensionsModCardModel
{
    public CosmicBlink() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(3, 2);
        WithVar("dazed", 3, -1);
        WithTip(typeof(Dazed));
        WithDimensionTags(DimensionType.Cosmic);
        WithTip(CardKeyword.Exhaust);
        WithTip(DimensionModKeyWords.Purge);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        
        await ExhaustPlaySystem.PlayFromExhaustPile(choiceContext, Owner, 1, null, true);
        
        await CreateCardCmd.GiveCards<Dazed>(Owner, DynamicVars["dazed"].IntValue, PileType.Exhaust, animationTime: 0.1f);
    }
}