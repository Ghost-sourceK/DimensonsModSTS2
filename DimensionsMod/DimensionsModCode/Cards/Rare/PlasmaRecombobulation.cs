using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Rare;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaRecombobulation : DimensionsModCardModel
{
    public PlasmaRecombobulation() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(10, 4);
        WithCards(2, 1);
        WithVar("MatterAggregation", 2);
        WithUpgradingCardTip<MatterAggregation>();
        WithDimensionTags(DimensionType.Plasma);
        WithTip(DimensionModKeyWords.Exhume);
        WithTip(DimensionModKeyWords.Void);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Plasma);
        }
        
        await CreateCardCmd.GiveCards<MatterAggregation>(Owner, DynamicVars["MatterAggregation"].IntValue, PileType.Draw, upgraded: IsUpgraded);
    }
}