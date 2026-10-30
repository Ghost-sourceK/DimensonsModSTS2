using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Ancients;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class DimensionDrawSystem
{
    private static readonly HashSet<CardModel> TriggeredThisTurn = new();
    
    public static async Task OnCardDrawn(CardModel card, PlayerChoiceContext choiceContext, Player player, params DimensionType[] sourceTags)
    {
        if (card.Pool is ColorlessCardPool || card.Rarity is CardRarity.Event)
            return;
        if (card.Rarity is CardRarity.Ancient && card is not OblivionDS)
            return;
        
        bool hasMatchingTag = card is DimensionsModCardModel dim && sourceTags.Any(dim.HasDimensionTag);
        if (hasMatchingTag)
        {
            if (TriggeredThisTurn.Add(card))
                await PlayerCmd.GainEnergy(1, player);

            return;
        }

        await CardCmd.Exhaust(choiceContext, card);
    }
    
    public static void ResetTurn()
    {
        TriggeredThisTurn.Clear();
    }
}