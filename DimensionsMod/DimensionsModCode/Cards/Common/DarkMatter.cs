using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkMatter : DimensionsModCardModel
{
    public DarkMatter() : base(-1, CardType.Attack, CardRarity.Common, TargetType.RandomEnemy)
    {
        WithDamage(7, 3);
        WithBlock(5, 2);
        WithDimensionTags(DimensionType.Dark);
        WithKeywords(CardKeyword.Unplayable);
        WithTip(PurgeKeyWord.Purge);
        WithTip(CardKeyword.Exhaust);
    }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
            return;
        
        int playCount = await GeneratePlayCount(CombatState, null);
        for (int i = 0; i < playCount; ++i)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block.IntValue, ValueProp.SkipHurtAnim, null);

            var target = CombatState.Enemies.FirstOrDefault(c => !c.IsDead);
            if (target == null)
                return;
            await DamageCmd.Attack(DynamicVars.Damage.IntValue).FromCard(this).Targeting(target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        }
    }
}
