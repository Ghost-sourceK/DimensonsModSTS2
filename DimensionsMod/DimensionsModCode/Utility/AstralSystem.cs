using DimensionsMod.DimensionsModCode.KeyWords;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class AstralSystem
{
    public static async Task PurgeAstralCards(Player owner)
    {
        var hand = PileType.Hand.GetPile(owner);
        
        var astralCards = hand.Cards.Where(card => card.IsAstral()).ToList();

        foreach (var card in astralCards.Where(card => card.Pile == hand))
        {
            await PurgeCardCmd.Purge(card);
        }
    }
}