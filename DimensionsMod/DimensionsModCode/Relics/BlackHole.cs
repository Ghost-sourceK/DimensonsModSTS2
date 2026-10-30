using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Abstract;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;

namespace DimensionsMod.DimensionsModCode.Relics;

[Pool(typeof(DimensionsModRelicPool))]
public sealed class BlackHole : DimensionsModRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override decimal ModifyHandDraw(Player player, decimal count)
    {
        return player != Owner || player.Creature.CombatState is { RoundNumber: > 1 } ? count : count + 2;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        BlackHole source = this;
        if (player != Owner || source.Owner.Creature.CombatState is { RoundNumber: > 1 })
            return;
        var list = (await CardSelectCmd.FromHandForDiscard(choiceContext, source.Owner, 
            new CardSelectorPrefs(source.SelectionScreenPrompt, 0, 1), null, source)).ToList();
        if (list.Count == 0)
            return;
        foreach (CardModel card in list)
        {
            await CardCmd.AutoPlay(choiceContext, card, null);
            await CardCmd.Exhaust(choiceContext, card);
        }

    }
}