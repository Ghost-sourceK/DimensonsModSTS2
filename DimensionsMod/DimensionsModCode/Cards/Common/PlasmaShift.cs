using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaShift : DimensionsModCardModel
{
    public PlasmaShift() : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(4, 2);
        WithVar("dazed", 3, -1);
        WithTip(typeof(Dazed));
        WithDimensionTags(DimensionType.Plasma);
        WithTip(CardKeyword.Exhaust);
        WithTip(DimensionModKeyWords.Purge);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        await ExhaustPlaySystem.PlayFromExhaustPile(choiceContext, Owner, 1, null, true);
        
        await CreateCardCmd.GiveCards<Dazed>(Owner, DynamicVars["dazed"].IntValue, PileType.Exhaust, animationTime: 0.1f);
    }
}