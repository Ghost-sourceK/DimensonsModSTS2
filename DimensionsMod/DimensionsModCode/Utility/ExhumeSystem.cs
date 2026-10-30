using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class ExhumeSystem
{
    public static async Task<IEnumerable<CardModel>?> ExhumeToHand
        (PlayerChoiceContext choiceContext, Player owner, int amount, LocString? prompt, bool random = false, bool reduceCost = false, CardModel? excludedCard = null)
    {
        var pile = PileType.Exhaust.GetPile(owner);

        if (pile.Cards.Count == 0)
            return null;
        
        var availableCards = pile.Cards.Where(c => c is not DormantStar && c != excludedCard && 
                                                c.Rarity != CardRarity.Ancient && c.Rarity != CardRarity.Event && c.Pool is not ColorlessCardPool).ToList();

        List<CardModel> selected;
        if (random)
        {
            selected = availableCards.OrderBy(_ => Guid.NewGuid()).Take(Math.Min(amount, availableCards.Count)).ToList();
        }
        else
        {
            var prefs = new CardSelectorPrefs(prompt!, amount);

            selected = (await CardSelectCmd.FromSimpleGrid(choiceContext, availableCards, owner, prefs)).ToList();
        }

        foreach (var card in selected)
        {
            if (card.Type is CardType.Status or CardType.Curse)
            {
                await PurgeCardCmd.Purge(card);
                continue;
            }
            
            await CardPileCmd.Add(card, PileType.Hand);
            
            var power = owner.Creature.Powers.OfType<StellarCataclysmPower>().FirstOrDefault();
            if (power != null)
                await StellarCataclysmPower.OnCardExhumed(card);
            
            if (reduceCost)
            {
                var currentCost = Math.Max(0, card.EnergyCost.GetAmountToSpend() - 1);
                card.EnergyCost.SetThisTurnOrUntilPlayed(currentCost, true);
            }
        }
        
        return selected;
    }
}