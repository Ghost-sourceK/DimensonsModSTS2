using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaCharge : DimensionsModCardModel
{
    public PlasmaCharge() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithCards(3, 1);
        WithDimensionTags(DimensionType.Plasma);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.Draw(this, choiceContext);
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
        CardModel? card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this)).FirstOrDefault();
        if (card == null)
            return;
        await CardCmd.Exhaust(choiceContext, card);
    }
}
