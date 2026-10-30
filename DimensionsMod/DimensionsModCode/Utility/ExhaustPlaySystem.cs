using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class ExhaustPlaySystem
{
    private static readonly HashSet<CardModel> ReservedCards = new();

    private static List<CardModel> GetAvailableCards(Player owner)
    {
        return PileType.Exhaust.GetPile(owner).Cards.Where(card => !ReservedCards.Contains(card)).ToList();
    }

    public static async Task<List<CardModel>?> PlayFromExhaustPile(PlayerChoiceContext choiceContext, Player owner, int amount, LocString? prompt, bool random)
    {
        var availableCards = GetAvailableCards(owner);
        
        if (availableCards.Count == 0)
            return null;
        
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
            ReservedCards.Add(card);
        
        foreach (var card in selected)
        {
            if (card.Keywords.Contains(CardKeyword.Unplayable))
                CardCmd.RemoveKeyword(card, CardKeyword.Unplayable);

            await CardCmd.AutoPlay(choiceContext, card, null);
            await PurgeCardCmd.Purge(card);
            ReservedCards.Remove(card);
        }
        
        return selected;
    }
}