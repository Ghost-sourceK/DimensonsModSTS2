using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicFlux : DimensionsModCardModel
{
    public CosmicFlux() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(7, 1);
        WithPower<VulnerablePower>(2,1);
        WithDimensionTags(DimensionType.Cosmic);
        WithTip(DimensionModKeyWords.Void);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        bool cosmicDrawn = false;
        var drawnCards = await CardPileCmd.Draw(choiceContext, 1, Owner);
        foreach (var card in drawnCards)
        {
            if (card is DimensionsModCardModel dim && dim.HasDimensionTag(DimensionType.Cosmic))
            {
                cosmicDrawn = true;
            }
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Cosmic);
        }
        
        if (cardPlay.Target != null && cosmicDrawn)
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext, cardPlay.Target, DynamicVars.Vulnerable.IntValue, Owner.Creature, this);
        }
    }
}
