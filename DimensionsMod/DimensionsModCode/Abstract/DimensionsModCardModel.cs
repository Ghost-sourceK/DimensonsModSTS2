using BaseLib.Abstracts;
using BaseLib.Extensions;
using DimensionsMod.DimensionsModCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using DimensionsMod.DimensionsModCode.Extensions;

namespace DimensionsMod.DimensionsModCode.Abstract;

public abstract class DimensionsModCardModel(
    int canonicalEnergyCost,
    CardType type,
    CardRarity rarity,
    TargetType targetType,
    bool shouldShowInCardLibrary = true) : ConstructedCardModel(canonicalEnergyCost, type, rarity, targetType, shouldShowInCardLibrary)
{
    private HashSet<DimensionType> DimensionTags { get; } = [];

    protected void WithDimensionTags(params DimensionType[] tags)
    {
        foreach (DimensionType tag in tags)
        {
            DimensionTags.Add(tag);
        }
    }

    public bool HasDimensionTag(DimensionType tag)
    {
        return DimensionTags.Contains(tag);
    }
    
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of full art: 250x350
    //Smaller variant of normal art: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}