using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.KeyWords;

public static class ConsumeKeyWord
{
    [CustomEnum]
    public static CardKeyword Consume;
    
    public static bool IsConsume(this CardModel card)
    {
        return card.Keywords.Contains(Consume);
    }
}