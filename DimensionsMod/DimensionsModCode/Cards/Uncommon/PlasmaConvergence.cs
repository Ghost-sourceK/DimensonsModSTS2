using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaConvergence : DimensionsModCardModel
{
    public PlasmaConvergence() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(14);
        WithCards(1, 1);
        WithVar("VoidParticles", 1, 1);
        WithDimensionTags(DimensionType.Plasma);
        WithTip(typeof(VoidParticles));
        WithTip(DimensionModKeyWords.Void);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Plasma);
        }
        
        await CreateCardCmd.GiveCards<VoidParticles>(Owner, DynamicVars["VoidParticles"].IntValue, PileType.Draw, CardPilePosition.Random, animationTime: 0.1f);
    }
}
