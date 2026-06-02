using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Rare;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkSanctuary : DimensionsModCardModel
{
    public DarkSanctuary() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithCards(1, 1);
        WithDimensionTags(DimensionType.Dark);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        DoublePlayPower? doublePlayPower = await PowerCmd.Apply<DoublePlayPower>(choiceContext, Owner.Creature, DynamicVars.Cards.IntValue, Owner.Creature, this);
        doublePlayPower?.SetTags(
            DimensionType.Dark
        );
    }
}