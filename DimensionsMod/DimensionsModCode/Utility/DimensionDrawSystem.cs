using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class DimensionDrawSystem
{
    private static readonly HashSet<CardModel> TriggeredThisTurn = new();
    
    public static async Task OnCardDrawn(CardModel card, PlayerChoiceContext choiceContext, Player player, DimensionType sourceTag)
    {
        if (card is DimensionsModCardModel dim)
        {
            if (dim.HasDimensionTag(sourceTag))
            {
                if (TriggeredThisTurn.Add(card))
                {
                    await PlayerCmd.GainEnergy(1, player);
                }
            }
            else
            {
                await CardCmd.Exhaust(choiceContext, card);
            }
        }
        else
        {
            await CardCmd.Exhaust(choiceContext, card);
        }
    }
    
    public static void ResetTurn()
    {
        TriggeredThisTurn.Clear();
    }
}