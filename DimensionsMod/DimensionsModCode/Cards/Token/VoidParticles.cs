using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DimensionsMod.DimensionsModCode.Cards.Token;

[Pool(typeof(DimensionsModCardPool))]
public sealed class VoidParticles : DimensionsModCardModel
{
    public VoidParticles() : base(-1, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithBlock(3, 1);
        WithCards(1, 1);
        WithKeywords(CardKeyword.Retain, CardKeyword.Unplayable);
        WithTip(ConsumeKeyWord.Consume);
        WithTip(PurgeKeyWord.Purge);
        WithTip(CardKeyword.Exhaust);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
        {
            await PurgeCardCmd.Purge(this);
            return;
        }
        
        int playCount = await GeneratePlayCount(CombatState, null);
        for (int i = 0; i < playCount; ++i)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block.IntValue, ValueProp.SkipHurtAnim, null);
            await CommonActions.Draw(this, choiceContext);
        }

        await PurgeCardCmd.Purge(this);
    }
}
