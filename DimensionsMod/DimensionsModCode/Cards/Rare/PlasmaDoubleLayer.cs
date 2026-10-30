using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Rare;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaDoubleLayer : DimensionsModCardModel
{
    public PlasmaDoubleLayer() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithCostUpgradeBy(-1);
        WithDimensionTags(DimensionType.Plasma);
        WithKeywords(CardKeyword.Exhaust);
        WithTip(DimensionModKeyWords.Purge);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var cardsInHand = PileType.Hand.GetPile(Owner).Cards.Where(card => card != this).ToList();

        foreach (var clone in cardsInHand.Select(card => card.CreateClone()))
        {
            if (!clone.IsPurge())
            {
                CardCmd.ApplyKeyword(clone, DimensionModKeyWords.Purge);
            }
            if (clone.Keywords.Contains(CardKeyword.Exhaust))
            {
                CardCmd.RemoveKeyword(clone, CardKeyword.Exhaust);
            }

            var result = await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Exhaust, Owner);
            CardCmd.PreviewCardPileAdd(result, 0.3f);
        }
    }
}