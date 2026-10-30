using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.KeyWords;

public static class DimensionModKeyWords
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Void;

    public static bool IsVoid(this CardModel card)
    {
        return card.Keywords.Contains(Void);
    }
    
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Astral;

    public static bool IsAstral(this CardModel card)
    {
        return card.Keywords.Contains(Astral);
    }
    
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Autoplay;

    public static bool IsAutoplay(this CardModel card)
    {
        return card.Keywords.Contains(Autoplay);
    }
    
    [CustomEnum]
    public static CardKeyword Consume;
    
    public static bool IsConsume(this CardModel card)
    {
        return card.Keywords.Contains(Consume);
    }
    
    [CustomEnum]
    public static CardKeyword Exhume;
    
    public static bool IsExhume(this CardModel card)
    {
        return card.Keywords.Contains(Exhume);
    }
    
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword HiggsBoson;

    public static bool IsHiggsBoson(this CardModel card)
    {
        return card.Keywords.Contains(HiggsBoson);
    }
    
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Purge;
    
    public static bool IsPurge(this CardModel card)
    {
        return card.Keywords.Contains(Purge);
    }
}