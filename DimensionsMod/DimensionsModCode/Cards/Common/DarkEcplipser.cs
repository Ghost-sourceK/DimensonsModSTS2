using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkEcplipser : DimensionsModCardModel
{
    public DarkEcplipser() : base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
    {
        WithDamage(6, 2);
        WithCalculatedDamage("DoubleDamage", 12, 1, (_, _) => 0, upgrade: 4);
        WithDimensionTags(DimensionType.Dark);
        WithTip(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_giant_horizontal_slash").Execute(choiceContext);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
            return;
        
        await DamageCmd.Attack(DynamicVars["DoubleDamage"].IntValue).FromCard(this, null).TargetingAllOpponents(CombatState).WithHitFx("vfx/vfx_giant_horizontal_slash").Execute(choiceContext);
    }
}
