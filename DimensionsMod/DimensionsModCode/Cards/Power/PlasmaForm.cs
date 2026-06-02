using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Power;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaForm : DimensionsModCardModel
{
    public PlasmaForm() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithDimensionTags(DimensionType.Plasma);
        WithKeyword(CardKeyword.Ethereal, UpgradeType.Remove);
        WithPower<PlasmaFormPower>(1);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.ApplySelf<PlasmaFormPower>(choiceContext, this);
    }
}