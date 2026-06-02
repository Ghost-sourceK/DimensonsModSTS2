using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Powers;

public sealed class DoublePlayPower : DimensionsModPower
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
    
    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        if (card.Owner.Creature != Owner || card is not DimensionsModCardModel dimCard)
            return playCount;

        return AllowedTags.Any(dimCard.HasDimensionTag) ? playCount + 1 : playCount;
    }

    public override async Task AfterModifyingCardPlayCount(CardModel card)
    {
        await PowerCmd.Decrement(this);
    }
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;
        await PowerCmd.Remove(this);
    }
}