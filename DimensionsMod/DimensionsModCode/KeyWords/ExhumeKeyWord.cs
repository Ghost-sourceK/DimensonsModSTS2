using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.KeyWords;

public static class ExhumeKeyWord
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Exhume;
    
    public static bool IsExhume(this CardModel card)
    {
        return card.Keywords.Contains(Exhume);
    }
}