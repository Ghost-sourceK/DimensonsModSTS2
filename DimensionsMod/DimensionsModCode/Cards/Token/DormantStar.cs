using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace DimensionsMod.DimensionsModCode.Cards.Token;

[Pool(typeof(TokenCardPool))]
public sealed class DormantStar : DimensionsModCardModel
{
    public DormantStar() : base(-1, CardType.Attack, CardRarity.Token, TargetType.RandomEnemy)
    {
        WithDamage(7, 3);
        WithBlock(3, 2);
        WithTip(DimensionModKeyWords.Exhume);
    }
    
    public override TargetType TargetType
    {
        get
        {
            if (!IsMutable || CombatState == null)
                return TargetType.RandomEnemy;

            return HasPlasmaForgePower ? TargetType.AllEnemies : TargetType.AnyEnemy;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        
        if (HasPlasmaForgePower)
        {
            if (CombatState == null)
                return;
            await DamageCmd.Attack(DynamicVars.Damage.IntValue).FromCard(this, null).TargetingAllOpponents(CombatState).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        }
        else
        {
            await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        }
    }
        
    private bool HasPlasmaForgePower => IsMutable && Owner.Creature.HasPower<PlasmaForgePower>();
}