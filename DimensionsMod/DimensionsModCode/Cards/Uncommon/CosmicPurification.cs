using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Utility;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicPurification : DimensionsModCardModel
{
    public CosmicPurification() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithBlock(8, 3);
        WithVar("Purge", 3, 1);
        WithDimensionTags(DimensionType.Cosmic);
        WithKeywords(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this, cardPlay);
        
        var exhaustPile = PileType.Exhaust.GetPile(Owner);
        var statuses = exhaustPile.Cards.Where(c => c.Type == CardType.Status).OrderBy
            (_ => Guid.NewGuid()).Take(DynamicVars["Purge"].IntValue).ToList();
        foreach (var card in statuses)
        {
            await PurgeCardCmd.Purge(card);
        }
    }
}