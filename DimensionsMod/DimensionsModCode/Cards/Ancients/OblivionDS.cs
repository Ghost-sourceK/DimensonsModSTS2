using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace DimensionsMod.DimensionsModCode.Cards.Ancients;

[Pool(typeof(DimensionsModCardPool))]
public sealed class OblivionDS : DimensionsModCardModel
{
    public OblivionDS() : base(1, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy)
    {
        WithDamage(14);
        WithCards(2);
        WithCostUpgradeBy(-1);
        WithDimensionTags(DimensionType.Cosmic, DimensionType.Plasma, DimensionType.Dark);
        WithTip(DimensionModKeyWords.Void);
        WithKeyword(DimensionModKeyWords.HiggsBoson);
        WithTip(DimensionModKeyWords.Exhume);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Cosmic, DimensionType.Plasma, DimensionType.Dark);
        }
        
        await ExhumeSystem.ExhumeToHand(choiceContext, Owner, DynamicVars.Cards.IntValue, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"));
    }
}