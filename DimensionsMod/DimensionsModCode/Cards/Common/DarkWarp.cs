using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkWarp : DimensionsModCardModel
{
    public DarkWarp() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithCards(2, 1);
        WithTip(typeof(Dazed));
        WithDimensionTags(DimensionType.Dark);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ExhumeSystem.ExhumeToHand(choiceContext, Owner, DynamicVars.Cards.IntValue, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"), true);
        await CreateCardCmd.GiveCards<Dazed>(Owner, 2, PileType.Exhaust, animationTime: 0.1f);
    }
}
