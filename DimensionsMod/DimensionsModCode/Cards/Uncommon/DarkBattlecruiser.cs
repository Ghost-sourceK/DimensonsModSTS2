using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkBattlecruiser : DimensionsModCardModel
{
    public DarkBattlecruiser() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(12, 4);
        WithVar("Exhume", 1, 1);
        WithVar("Exhaust", 1);
        WithDimensionTags(DimensionType.Dark);
        WithTip(DimensionModKeyWords.Exhume);
        WithTip(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, DynamicVars["Exhaust"].IntValue);
        foreach (CardModel card in await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this))
            await CardCmd.Exhaust(choiceContext, card);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
            return;
        
        await ExhumeSystem.ExhumeToHand(choiceContext, Owner, DynamicVars["Exhume"].IntValue, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"), excludedCard: cardModel);
    }
}
