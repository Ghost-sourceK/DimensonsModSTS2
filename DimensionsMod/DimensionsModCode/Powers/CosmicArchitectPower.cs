using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace DimensionsMod.DimensionsModCode.Powers;

public sealed class CosmicArchitectPower : DimensionsModPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    private readonly List<bool> _architectStacks = new();
    private ICombatState? _lastCombatState;

    public void AddArchitectStack(bool upgraded, int amount = 0)
    {
        ICombatState? currentCombatState = Owner.CombatState;

        if (!ReferenceEquals(_lastCombatState, currentCombatState))
        {
            _architectStacks.Clear();
            _lastCombatState = currentCombatState;
        }

        for (int i = 0; i < amount; i++)
        {
            _architectStacks.Add(upgraded);
        }
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {
        if (player != Owner.Player)
            return;
        
        foreach (bool upgraded in _architectStacks)
        {
            var card = combatState.CreateCard<DormantStar>(Owner.Player);

            if (upgraded)
                CardCmd.Upgrade(card);

            CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(card, PileType.Exhaust), 0.5f);
        }
    }
}