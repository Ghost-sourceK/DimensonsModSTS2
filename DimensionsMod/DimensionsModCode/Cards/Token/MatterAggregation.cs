using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace DimensionsMod.DimensionsModCode.Cards.Token;

[Pool(typeof(DimensionsModCardPool))]
public sealed class MatterAggregation : DimensionsModCardModel
{
    public MatterAggregation() : base(-1, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithBlock(2, 2);
        WithVars(new EnergyVar(1));
        WithEnergyTip();
        WithKeywords(CardKeyword.Ethereal, CardKeyword.Unplayable);
        //WithTip(AstralKeyWord.Astral);
        WithTip(ConsumeKeyWord.Consume);
        WithTip(PurgeKeyWord.Purge);
    }
    
    public static IEnumerable<MatterAggregation?> Create(Player owner, int amount, ICombatState? combatState)
    {
        var matterAggregationList = new List<MatterAggregation?>();
        for (int index = 0; index < amount; ++index)
            matterAggregationList.Add(combatState?.CreateCard<MatterAggregation>(owner));
        return matterAggregationList;
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
            await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
        }

        await PurgeCardCmd.Purge(this);
    }
}
