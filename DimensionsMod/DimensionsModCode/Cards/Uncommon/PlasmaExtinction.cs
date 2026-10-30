using BaseLib.Extensions;
using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaExtinction : DimensionsModCardModel
{
    protected override bool ShouldGlowRedInternal => Cards2Draw;
    
    public PlasmaExtinction() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithVar("Exhaust", 2, 1);
        WithVars(new EnergyVar(1).WithUpgrade(1));
        WithEnergyTip();
        WithDimensionTags(DimensionType.Plasma);
        WithTip(CardKeyword.Exhaust);
    }
    
    private bool Cards2Draw => Owner.PlayerCombatState?.DrawPile.Cards.Count < (IsUpgraded ? 3 : 2);

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var drawPile = PileType.Draw.GetPile(Owner).Cards.ToList();
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, DynamicVars["Exhaust"].IntValue, DynamicVars["Exhaust"].IntValue);
        var selectedCards = (await CardSelectCmd.FromSimpleGrid(choiceContext, drawPile, Owner, prefs )).ToList();
        foreach (var card in selectedCards)
        {
            await CardCmd.Exhaust(choiceContext, card);
        }
        
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
    }
    
    protected override bool IsPlayable
    {
        get
        {
            int requiredCards = DynamicVars["Exhaust"].IntValue;
            int availableCards = PileType.Draw.GetPile(Owner).Cards.Count;

            return availableCards >= requiredCards;
        }
    }
}