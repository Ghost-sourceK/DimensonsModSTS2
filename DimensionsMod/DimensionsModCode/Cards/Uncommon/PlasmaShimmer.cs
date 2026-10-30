using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaShimmer : DimensionsModCardModel
{
    public PlasmaShimmer() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithDamage(15, 5);
        WithVar("VoidParticles", 2);
        WithVar("Exhaust", 1, 1);
        WithTip(typeof(VoidParticles));
        WithDimensionTags(DimensionType.Plasma);
        WithTip(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, DynamicVars["Exhaust"].IntValue);
        foreach (CardModel card in await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this))
            await CardCmd.Exhaust(choiceContext, card);
        
        await CreateCardCmd.GiveCards<MatterAggregation>(Owner, DynamicVars["VoidParticles"].IntValue, PileType.Draw);
    }
}
