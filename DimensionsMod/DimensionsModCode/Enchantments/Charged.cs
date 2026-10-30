using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Enchantments;

public sealed class Charged : CustomEnchantmentModel
{
    public override bool HasExtraCardText => true;
    public override bool ShowAmount => false;

    public override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay? cardPlay)
    {
        if (cardPlay == null)
            return;
        
        await CardPileCmd.Draw(choiceContext, 1, cardPlay.Card.Owner);
    }
}