using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DimensionsMod.DimensionsModCode.Powers;

public sealed class CosmicPhantomStarPower : DimensionsModPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    private bool _upgraded;
    private bool _createdDormantStarThisPlay;

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (creator != Owner.Player || card is not Dazed)
            return;
        if (Owner.Player == null || _createdDormantStarThisPlay)
            return;

        _createdDormantStarThisPlay = true;
        
        for (int i = 0; i < Amount; i++)
        {
            var dormantStar = Owner.Player.Creature.CombatState?.CreateCard<DormantStar>(Owner.Player);

            if (dormantStar != null)
            {
                if (_upgraded)
                    CardCmd.Upgrade(dormantStar);

                var result = await CardPileCmd.Add(dormantStar, PileType.Exhaust);
                CardCmd.PreviewCardPileAdd(result, 0.1f);
            }
        }
    }


    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        _createdDormantStarThisPlay = false;
        return Task.CompletedTask;
    }

    public void SetUpgraded(bool upgraded)
    {
        _upgraded = upgraded;
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (Owner.Side == side)
            return;

        await PowerCmd.Remove(this);
    }
}