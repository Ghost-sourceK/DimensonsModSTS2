using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaRecursion : DimensionsModCardModel
{
    private decimal _extraDamageFromExhaust;
    
    public PlasmaRecursion() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(7, 3);
        WithVar("damageBonus", 3, 1);
        WithDimensionTags(DimensionType.Plasma);
        WithTip(CardKeyword.Exhaust);
    }
    
    private decimal ExtraDamageFromExhaust
    {
        get => _extraDamageFromExhaust;
        set
        {
            AssertMutable();
            _extraDamageFromExhaust = value;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
            return;
        
        DamageVar damage = DynamicVars.Damage;
        damage.BaseValue += DynamicVars["damageBonus"].IntValue;
        _extraDamageFromExhaust += DynamicVars["damageBonus"].IntValue;
        
        PileType.Exhaust.GetPile(Owner).RemoveInternal(this);
        CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(this, PileType.Draw, CardPilePosition.Random));
    }
    
    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        DamageVar damage = DynamicVars.Damage;
        damage.BaseValue += ExtraDamageFromExhaust;
    }
}