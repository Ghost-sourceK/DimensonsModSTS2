using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.KeyWords;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Rare;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicTranscendence : DimensionsModCardModel
{
    public CosmicTranscendence() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithCards(3, 1);
        WithVar("scry", 5, 1);
        WithDimensionTags(DimensionType.Cosmic);
        WithTip(VoidKeyWord.Void);
        WithTip(ScryKeyWord.Scry);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ScryCmd.Execute(choiceContext, Owner, DynamicVars["scry"].IntValue);
        
        var drawnCards = await CommonActions.Draw(this, choiceContext);
        foreach (var card in drawnCards)
        {
            await DimensionDrawSystem.OnCardDrawn(card, choiceContext, Owner, DimensionType.Cosmic);
        }
    }
}