using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Powers;

public sealed class FreeDimensionPower : DimensionsModPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    private HashSet<DimensionType> AllowedTags { get; } = [];

    public void SetTags(params DimensionType[] tags)
    {
        AllowedTags.Clear();

        foreach (var tag in tags)
        {
            AllowedTags.Add(tag);
        }
    }

    public override bool TryModifyEnergyCostInCombatLate( CardModel card, decimal originalCost, out decimal modifiedCost)
    {
        modifiedCost = originalCost;
        
        if (card is not DimensionsModCardModel dimCard)
            return false;
        
        bool validTag = AllowedTags.Any(dimCard.HasDimensionTag);

        if (!validTag)
            return false;

        modifiedCost = 0;

        return true;
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card is not DimensionsModCardModel dimCard)
            return;

        bool validTag = AllowedTags.Any(dimCard.HasDimensionTag);

        if (!validTag)
            return;

        await PowerCmd.Decrement(this);
    }
}