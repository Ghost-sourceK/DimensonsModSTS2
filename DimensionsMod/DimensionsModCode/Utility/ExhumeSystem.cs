using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class ExhumeSystem
{
    public static async Task<IEnumerable<CardModel>?> ExhumeToHand
        (PlayerChoiceContext choiceContext, Player owner, int amount, LocString prompt, bool random = false, bool reduceCost = false)
    {
        var pile = PileType.Exhaust.GetPile(owner);

        if (pile.Cards.Count == 0)
            return null;

        List<CardModel> selected;
        if (random)
        {
            selected = pile.Cards.OrderBy(_ => Guid.NewGuid()).Take(Math.Min(amount, pile.Cards.Count)).ToList();
        }
        else
        {
            var prefs = new CardSelectorPrefs(prompt, amount);

            selected = (await CardSelectCmd.FromSimpleGrid(choiceContext, pile.Cards, owner, prefs)).ToList();
        }

        foreach (var card in selected)
        {
            if (card.Type is CardType.Status or CardType.Curse)
            {
                await PurgeCardCmd.Purge(card);
                continue;
            }
            
            await CardPileCmd.Add(card, PileType.Hand);
            
            if (reduceCost)
            {
                var currentCost = Math.Max(0, card.EnergyCost.GetAmountToSpend() - 1);
                card.EnergyCost.SetThisTurnOrUntilPlayed(currentCost, true);
            }
        }
        
        return selected;
    }
}