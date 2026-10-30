using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkWarp : DimensionsModCardModel
{
    public DarkWarp() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithDamage(7, 3);
        WithVar("Exhume", 2, 1);
        WithTip(typeof(Dazed));
        WithTip(DimensionModKeyWords.Exhume);
        WithDimensionTags(DimensionType.Dark);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ExhumeSystem.ExhumeToHand(choiceContext, Owner, DynamicVars["Exhume"].IntValue, null, true);
        await CreateCardCmd.GiveCards<Dazed>(Owner, 2, PileType.Exhaust, animationTime: 0.1f);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
            return;
        
        var enemies = Owner.Creature.CombatState?.HittableEnemies.Where(e => e.IsAlive).ToList();
        if (enemies is not { Count: > 0 })
            return;
        var randomTarget = enemies[Owner.RunState.Rng.Shuffle.NextInt(enemies.Count)];
        
        await CreatureCmd.Damage(choiceContext, randomTarget, DynamicVars.Damage, this, null);
    }
}
