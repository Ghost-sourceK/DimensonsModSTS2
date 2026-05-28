using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;

namespace DimensionsMod.DimensionsModCode.Cards.Basic;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaReserve : DimensionsModCardModel
{
    public PlasmaReserve() : base(2, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        WithBlock(12, 4);
        WithCards(1);
        WithDimensionTags(DimensionType.Plasma);
        WithTip(VoidKeyWord.Void);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Cosmic);
        }
    }
}
