using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicRay : DimensionsModCardModel
{
    public CosmicRay() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(7, 2);
        WithCards(1);
        WithUpgradingCardTip<MatterAggregation>();
        WithDimensionTags(DimensionType.Cosmic);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CosmicRay card1 = this;
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        var cards = MatterAggregation.Create(card1.Owner, card1.DynamicVars.Cards.IntValue, card1.CombatState).ToList();
        if (card1.IsUpgraded)
        {
            foreach (MatterAggregation? card2 in cards)
                CardCmd.Upgrade(card2!);
        }
        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) cards, PileType.Discard, card1.Owner));
    }
}