using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaBlast : DimensionsModCardModel
{
    public PlasmaBlast() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithBlock(6);
        WithCards(1, 1);
        WithVar("Debuff", 1, 1);
        WithDimensionTags(DimensionType.Plasma);
        WithTip(DimensionModKeyWords.Void);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        
        bool cosmicDrawn = false;
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            if (card is DimensionsModCardModel dim && dim.HasDimensionTag(DimensionType.Plasma))
            {
                cosmicDrawn = true;
            }
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Plasma);
        }
        
        if (cosmicDrawn)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
            foreach (Creature hittableEnemy in CombatState?.HittableEnemies!)
            {
                await PowerCmd.Apply<WeakPower>(choiceContext, hittableEnemy, DynamicVars["Debuff"].IntValue, Owner.Creature, this);
            }
        }
    }
}
