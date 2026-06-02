using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Cards.Common;

[Pool(typeof(DimensionsModCardPool))]
public sealed class PlasmaDuplication : DimensionsModCardModel
{
    public PlasmaDuplication() : base(-1, CardType.Attack, CardRarity.Common, TargetType.Self)
    {
        WithCards(2,1);
        WithTip(typeof(VoidParticles));
        WithKeywords(CardKeyword.Exhaust);
        WithDimensionTags(DimensionType.Plasma);
        WithTip(AutoplayKeyWord.Autoplay);
    }
    
    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel drawnCard, bool fromHandDraw)
    {
        if (drawnCard != this) return;
        await CreateCardCmd.GiveCards<VoidParticles>(Owner, DynamicVars.Cards.IntValue, PileType.Hand, animationTime: 0.1f);
        await CardCmd.Exhaust(choiceContext, this);
    }
}