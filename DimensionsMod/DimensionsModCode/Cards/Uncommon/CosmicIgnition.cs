using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Abstract;
using DimensionsMod.DimensionsModCode.Cards.Token;
using DimensionsMod.DimensionsModCode.Character;
using DimensionsMod.DimensionsModCode.Enchantments;
using DimensionsMod.DimensionsModCode.KeyWords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DimensionsMod.DimensionsModCode.Cards.Uncommon;

[Pool(typeof(DimensionsModCardPool))]
public sealed class CosmicIgnition : DimensionsModCardModel
{
    public CosmicIgnition() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(8,4);
        WithVar("Enchant", 2, 1);
        WithTip(typeof(DormantStar));
        WithTip(EnchantmentsKeyWord.Charged);
        WithDimensionTags(DimensionType.Cosmic);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay).WithHitFx("vfx/vfx_starry_impact").Execute(choiceContext);
        
        var exhaustPile = PileType.Exhaust.GetPile(Owner);
        var candidates = exhaustPile.Cards.Where(card => card is DormantStar).Where(card => card.Enchantment == null).ToList();

        int count = Math.Min(DynamicVars["Enchant"].IntValue, candidates.Count);
        var selectedCards = candidates.StableShuffle(Owner.RunState.Rng.Shuffle).Take(count).ToList();
        
        foreach (var card in selectedCards)
        {
            CardCmd.Enchant<Charged>(card, 0m);
        }
    }
}
