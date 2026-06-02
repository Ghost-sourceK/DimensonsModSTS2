using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Abstract;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace DimensionsMod.DimensionsModCode.Relics;

[Pool(typeof(DimensionsModRelicPool))]
public sealed class RedGiant : DimensionsModRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    
    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        return player != Owner || player.Creature.CombatState is { RoundNumber: > 1 } ? count : count + 1;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        RedGiant source = this;
        if (player != Owner || source.Owner.Creature.CombatState is { RoundNumber: > 1 }) 
            return;
        CardPile pile = PileType.Hand.GetPile(source.Owner);
        CardModel? card = source.Owner.RunState.Rng.CombatCardSelection.NextItem(pile.Cards);
        if (card == null)
            return;
        await CardCmd.Exhaust(choiceContext, card);
    }

    public override RelicModel? GetUpgradeReplacement()
    {
        return ModelDb.Relic<BlackHole>();
    }
}