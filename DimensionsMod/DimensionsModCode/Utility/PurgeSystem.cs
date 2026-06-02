using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class PurgeSystem
{
    public static async Task<IEnumerable<CardModel>?> PurgeFromExhaust(
        PlayerChoiceContext choiceContext,
        Player owner,
        int amount,
        LocString prompt)
    {
        var pile = PileType.Exhaust.GetPile(owner);

        if (pile.Cards.Count == 0)
            return null;

        var prefs = new CardSelectorPrefs(prompt, Math.Min(amount, pile.Cards.Count));

        var selected = (await CardSelectCmd.FromSimpleGrid(choiceContext, pile.Cards, owner, prefs)).ToList();

        foreach (var card in selected)
        {
            await PurgeCardCmd.Purge(card);
        }

        return selected;
    }
}