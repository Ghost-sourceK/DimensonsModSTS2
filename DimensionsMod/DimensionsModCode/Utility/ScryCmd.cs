using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class ScryCmd
{
    public static async Task Execute(PlayerChoiceContext choiceContext, Player player, int amount)
    {
        if (amount <= 0) return;

        var drawPile = PileType.Draw.GetPile(player);
        var cardsToScry = drawPile.Cards.Take(amount).ToList();

        if (cardsToScry.Count == 0) return;
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 0, cardsToScry.Count);

        var cardsToDiscard = (await CardSelectCmd.FromSimpleGrid( choiceContext, cardsToScry, player, prefs)).ToList();
        foreach (var card in cardsToDiscard) await CardCmd.Discard(choiceContext, card);
        await DimensionsModHook.OnScryed(choiceContext, player, amount, cardsToDiscard.Count);
    }
}