using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkDestroyer : DimensionsModCardModel
{
    public DarkDestroyer() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(14, 6);
        WithEnergyTip();
        WithDimensionTags(DimensionType.Dark);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_heavy_blunt").Execute(choiceContext);
        
        FreeDimensionPower? freeDimensionPower = await PowerCmd.Apply<FreeDimensionPower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
        freeDimensionPower?.SetTags(
            DimensionType.Cosmic,
            DimensionType.Plasma
        );
    }
}
