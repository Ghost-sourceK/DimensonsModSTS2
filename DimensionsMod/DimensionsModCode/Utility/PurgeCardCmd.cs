using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Utility;

public static class PurgeCardCmd
{
    public static async Task Purge(CardModel card)
    {
        var pile = card.Pile;

        pile?.RemoveInternal(card);

        await Task.CompletedTask;
    }
}