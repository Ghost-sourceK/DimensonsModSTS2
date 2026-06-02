using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.KeyWords;

public static class AutoplayKeyWord
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Autoplay;

    public static bool IsAutoplay(this CardModel card)
    {
        return card.Keywords.Contains(Autoplay);
    }
}