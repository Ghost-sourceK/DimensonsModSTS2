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
public sealed class CosmicSingularity : DimensionsModCardModel
{
    public CosmicSingularity() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(10, 4);
        WithBlock(10, 4);
        WithCards(2);
        WithDimensionTags(DimensionType.Cosmic);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        await CommonActions.CardBlock(this, cardPlay);
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 2);
        foreach (CardModel card in await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this))
            await CardCmd.Exhaust(choiceContext, card);
    }
}
