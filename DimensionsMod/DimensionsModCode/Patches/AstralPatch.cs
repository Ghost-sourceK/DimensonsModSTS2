using System.Threading.Tasks;
using DimensionsMod.DimensionsModCode.Utility;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Hooks;

namespace DimensionsMod.DimensionsModCode.Patches;

[HarmonyPatch(typeof(Hook), nameof(Hook.BeforeFlush))]
public static class AstralPatch
{
    [HarmonyPostfix]
    public static void Postfix(ICombatState combatState, Player player, ref Task __result)
    {
        __result = PurgeAfterBeforeFlush(__result, player);
    }

    private static async Task PurgeAfterBeforeFlush(Task originalTask, Player player)
    {
        await originalTask;

        await AstralSystem.PurgeAstralCards(player);
    }
}