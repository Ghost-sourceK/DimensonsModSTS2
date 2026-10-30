using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicWormhole : DimensionsModCardModel
{
    public CosmicWormhole() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(7, 2);
        WithVars(new RepeatVar(2));
        WithDimensionTags(DimensionType.Cosmic);
        WithTip(DimensionModKeyWords.Purge);
        WithEnergyTip();
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitCount(DynamicVars.Repeat.IntValue).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        CardModel clone = CreateClone();
        clone.EnergyCost.SetThisCombat(1);
        if (!clone.IsPurge())
        {
            CardCmd.ApplyKeyword(clone, DimensionModKeyWords.Purge);
        }

        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Exhaust, Owner), 0.6f);
        
        if (this.IsPurge())
        {
            await CardCmd.Exhaust(choiceContext, this);
        }
    }
}