using DimensionsMod.DimensionsModCode.Abstract;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Powers;

public sealed class StellarCataclysmPower : DimensionsModPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public static async Task OnCardExhumed(CardModel card)
    {
        CardModel clone = card.CreateClone();

        var result = await CardPileCmd.Add(clone, PileType.Exhaust);

        CardCmd.PreviewCardPileAdd(result, 0.4f);
    }
}