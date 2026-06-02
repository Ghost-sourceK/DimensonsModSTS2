using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicQuasar : DimensionsModCardModel
{
    public CosmicQuasar() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(8, 4);
        WithCards(1);
        WithKeywords(CardKeyword.Exhaust);
        WithDimensionTags(DimensionType.Cosmic);
        WithTip(VoidKeyWord.Void);
        WithTip(ExhumeKeyWord.Exhume);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Cosmic);
        }
        
        await ExhumeSystem.ExhumeToHand(choiceContext, Owner, 1, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"));
    }
}
