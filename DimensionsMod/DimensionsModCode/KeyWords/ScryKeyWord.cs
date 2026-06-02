using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.KeyWords;

public static class ScryKeyWord
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Scry;

    public static bool IsScry(this CardModel card)
    {
        return card.Keywords.Contains(Scry);
    }
}