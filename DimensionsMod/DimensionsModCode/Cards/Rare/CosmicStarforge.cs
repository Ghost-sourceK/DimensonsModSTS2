using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Powers;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Rare;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicStarforge : DimensionsModCardModel
{
    public CosmicStarforge() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithUpgradingCardTip<DormantStar>();
        WithPower<PlasmaForgePower>(1);
        WithDimensionTags(DimensionType.Cosmic);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.ApplySelf<PlasmaForgePower>(choiceContext, this);
        
        await CreateCardCmd.GiveCards<DormantStar>(Owner, 3, PileType.Exhaust, upgraded: IsUpgraded, animationTime: 0.2f);
    }
}