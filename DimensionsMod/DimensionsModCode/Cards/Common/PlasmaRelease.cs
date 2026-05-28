using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaRelease : DimensionsModCardModel
{
    public PlasmaRelease() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(4, 2);
        WithCards(2);
        WithDimensionTags(DimensionType.Plasma);
        WithKeywords(CardKeyword.Exhaust);
        WithTip(VoidKeyWord.Void);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Plasma);
        }
    }
}