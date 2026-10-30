using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Rare;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaForm : DimensionsModCardModel
{
    public PlasmaForm() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<PlasmaFormPower>(1);
        WithDimensionTags(DimensionType.Plasma);
        WithKeyword(DimensionModKeyWords.Astral);
        WithTip(DimensionModKeyWords.Exhume);
        WithTip(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.ApplySelf<PlasmaFormPower>(choiceContext, this, DynamicVars["PlasmaFormPower"].IntValue);
    }
    
    protected override void OnUpgrade()
    {
        RemoveKeyword(DimensionModKeyWords.Astral);
        AddKeyword(CardKeyword.Ethereal);
    }
}