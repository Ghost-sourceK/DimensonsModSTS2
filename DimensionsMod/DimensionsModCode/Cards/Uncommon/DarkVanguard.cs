using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkVanguard : DimensionsModCardModel
{
    public DarkVanguard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithCards(2, 1);
        WithKeywords(CardKeyword.Exhaust);
        WithDimensionTags(DimensionType.Dark);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        foreach (Creature hittableEnemy in CombatState?.HittableEnemies!)
        {
            await PowerCmd.Apply<PiercingWailPower>(choiceContext, hittableEnemy, 6, Owner.Creature, this);
        }
        
        await ExhumeSystem.ExhumeToHand(choiceContext, Owner, DynamicVars.Cards.IntValue, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"));
    }
}
