using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicArmory : DimensionsModCardModel
{
    public CosmicArmory() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(5, 2);
        WithDimensionTags(DimensionType.Cosmic);
        WithKeywords(CardKeyword.Exhaust);
        WithTip( PurgeKeyWord.Purge);
        WithTip( ConsumeKeyWord.Consume);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }
    
    protected override PileType GetResultPileTypeForCardPlay()
    {
        return IsUpgraded ? PileType.Exhaust : PileType.None;
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        foreach (CardModel card in PileType.Hand.GetPile(Owner).Cards.Where(c => c.IsUpgradable))
            CardCmd.Upgrade(card);
    }
}
