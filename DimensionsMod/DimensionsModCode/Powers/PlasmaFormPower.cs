using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Powers;

public sealed class PlasmaFormPower : DimensionsModPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player)
            return;

        var exhaustPile = PileType.Exhaust.GetPile(player);

        if (exhaustPile.Cards.Count == 0)
            return;

        await ExhumeSystem.ExhumeToHand(choiceContext, player, Amount, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"));

        var hand = PileType.Hand.GetPile(player);

        if (hand.Cards.Count == 0)
            return;

        var prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, Amount);

        var selectedCards = (await CardSelectCmd.FromHand(choiceContext, player, prefs, null, this)).ToList();
        foreach (CardModel card in selectedCards)
        {
            await CardCmd.Exhaust(choiceContext, card);
        }
    }
}