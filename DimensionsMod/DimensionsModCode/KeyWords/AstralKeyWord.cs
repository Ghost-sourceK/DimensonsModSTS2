using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.KeyWords;

public static class AstralKeyWord
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Astral;

    public static bool IsAstral(this CardModel card)
    {
        return card.Keywords.Contains(Astral);
    }
}