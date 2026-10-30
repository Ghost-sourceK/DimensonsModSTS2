using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Common;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Rare;

[Pool(typeof(DimensionsModCardPool))]
public sealed class DarkCarrier : DimensionsModCardModel
{
    public DarkCarrier() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(12, 2);
        WithVar("PlayPurge", 2,1);
        WithVar("DarkMatter", 2);
        WithTip(typeof(DarkMatter));
        WithDimensionTags(DimensionType.Dark);
        WithTip(DimensionModKeyWords.Purge);
        WithTip(CardKeyword.Ethereal);
        WithTip(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        await ExhaustPlaySystem.PlayFromExhaustPile(choiceContext, Owner, DynamicVars["PlayPurge"].IntValue, new LocString("cards", "DIMENSIONSMOD-EXHAUST_SELECT_PROMPT"), false);
    }
    
    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel cardModel, bool causedByEthereal)
    {
        if (cardModel != this || CombatState == null)
            return;
        if (causedByEthereal)
            return;
        
        for (int i = 0; i < DynamicVars["DarkMatter"].IntValue; i++)
        {
            CardModel darkMatter = CombatState.CreateCard<DarkMatter>(Owner);
            if (!darkMatter.Keywords.Contains(CardKeyword.Ethereal))
            {
                CardCmd.ApplyKeyword(darkMatter, CardKeyword.Ethereal);
            }

            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(darkMatter, PileType.Draw, Owner, CardPilePosition.Random), 0.5f);
        }
        
    }
}