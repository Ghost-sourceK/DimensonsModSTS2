using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicExoplanet : DimensionsModCardModel
{
    public CosmicExoplanet() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(7, 3);
        WithCards(1);
        WithDimensionTags(DimensionType.Cosmic);
        WithTip(ExhumeKeyWord.Exhume);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        
        var pile = PileType.Exhaust.GetPile(Owner);
        var prefs = new CardSelectorPrefs(new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"), 1);

        CardModel? card = (await CardSelectCmd.FromSimpleGrid( choiceContext, pile.Cards, Owner, prefs )).FirstOrDefault();

        if (card == null)
            return;

        await CardPileCmd.Add(card, PileType.Hand);
    }
}
