using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaSaturation : DimensionsModCardModel
{
    public PlasmaSaturation() : base(-1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<StrengthPower>(2, 1);
        WithPower<DexterityPower>(2, 1);
        WithKeywords(CardKeyword.Unplayable);
        WithTip(CardKeyword.Exhaust);
        WithTip(PurgeKeyWord.Purge);
        WithDimensionTags(DimensionType.Plasma);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
            return;
        
        int playCount = await GeneratePlayCount(CombatState, null);
        for (int i = 0; i < playCount; ++i)
        {
            await CommonActions.ApplySelf<StrengthPower>(choiceContext, this);
            await CommonActions.ApplySelf<DexterityPower>(choiceContext, this);
        }
        
    }
}
