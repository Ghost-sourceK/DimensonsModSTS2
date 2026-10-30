using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Ancients;

[Pool(typeof(DimensionsModCardPool))]
public sealed class 
    StellarCataclysm: DimensionsModCardModel
{
    public StellarCataclysm() : base(3, CardType.Power, CardRarity.Ancient, TargetType.Self)
    {
        WithPower<StellarCataclysmPower>(1);
        WithDimensionTags(DimensionType.Cosmic, DimensionType.Plasma, DimensionType.Dark);
        WithKeywords(DimensionModKeyWords.Astral, DimensionModKeyWords.HiggsBoson);
        WithTip(DimensionModKeyWords.Exhume);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.ApplySelf<StellarCataclysmPower>(choiceContext, this, DynamicVars["StellarCataclysmPower"].IntValue);
    }
    
    protected override void OnUpgrade()
    {
        RemoveKeyword(DimensionModKeyWords.Astral);
        AddKeyword(CardKeyword.Ethereal);
    }
}