using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaFusion : DimensionsModCardModel
{
    public PlasmaFusion() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithEnergy(1);
        WithCards(0,1);
        WithEnergyTip();
        WithDimensionTags(DimensionType.Plasma);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
        await CommonActions.Draw(this, choiceContext);
        
        await PurgeSystem.PurgeFromExhaust(choiceContext, Owner, 1, new LocString("cards", "DIMENSIONSMOD-PURGE_EXHAUST_PILE_PROMPT"));
    }
}