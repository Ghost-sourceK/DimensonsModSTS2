using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;

namespace DimensionsMod.DimensionsModCode.Cards.Basic;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicDarkness : DimensionsModCardModel
{
    public CosmicDarkness() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        WithDamage(9);
        WithCards(1, 1);
        WithDimensionTags(DimensionType.Cosmic);
        WithTip(VoidKeyWord.Void);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_attack_blunt").Execute(choiceContext);
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Cosmic);
        }
    }
}
