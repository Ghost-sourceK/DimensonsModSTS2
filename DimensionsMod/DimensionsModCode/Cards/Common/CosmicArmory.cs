using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
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
        WithTip(CardKeyword.Exhaust);
        WithKeyword(ConsumeKeyWord.Consume, UpgradeType.Remove);
        WithTip(PurgeKeyWord.Purge);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
        {
            if (!IsUpgraded)
                await PurgeCardCmd.Purge(this);
            return;
        }

        int playCount = await GeneratePlayCount(CombatState, null);
        for (int i = 0; i < playCount; ++i)
        {
            foreach (CardModel card in PileType.Hand.GetPile(Owner).Cards.Where(c => c.IsUpgradable))
                CardCmd.Upgrade(card);
        }

        if (!IsUpgraded)
            await PurgeCardCmd.Purge(this);
    }
}
