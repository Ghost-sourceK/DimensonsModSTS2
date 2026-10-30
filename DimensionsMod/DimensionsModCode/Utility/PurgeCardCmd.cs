using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class PurgeCardCmd
{
    public static async Task Purge(CardModel card)
    {
        if (card.Pile == null)
            return;

        await CardPileCmd.RemoveFromCombat(card);
    }
}