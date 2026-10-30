using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicPhantomStar : DimensionsModCardModel
{
    public CosmicPhantomStar() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<CosmicPhantomStarPower>(1);
        WithUpgradingCardTip<DormantStar>();
        WithTip(typeof(Dazed));
        WithKeyword(CardKeyword.Exhaust);
        WithDimensionTags(DimensionType.Cosmic);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var power = await CommonActions.ApplySelf<CosmicPhantomStarPower>(choiceContext, this);
        power?.SetUpgraded(IsUpgraded);
    }
}