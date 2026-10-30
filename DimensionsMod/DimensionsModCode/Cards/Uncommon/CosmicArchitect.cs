using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicArchitect : DimensionsModCardModel
{
    public CosmicArchitect() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithDimensionTags(DimensionType.Cosmic);
        WithUpgradingCardTip<DormantStar>();
        WithPower<CosmicArchitectPower>(1);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CosmicArchitectPower? power = await CommonActions.ApplySelf<CosmicArchitectPower>(choiceContext, this, DynamicVars["CosmicArchitectPower"].IntValue);
        power?.AddArchitectStack(IsUpgraded, DynamicVars["CosmicArchitectPower"].IntValue);
    }
}