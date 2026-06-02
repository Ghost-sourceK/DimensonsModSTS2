using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkBattlecruiser : DimensionsModCardModel
{
    public DarkBattlecruiser() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(12, 2);
        WithCards(1, 1);
        WithDimensionTags(DimensionType.Dark);
        WithTip(ExhumeKeyWord.Exhume);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        await ExhumeSystem.ExhumeToHand(choiceContext, Owner, DynamicVars.Cards.IntValue, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"));
    }
}
