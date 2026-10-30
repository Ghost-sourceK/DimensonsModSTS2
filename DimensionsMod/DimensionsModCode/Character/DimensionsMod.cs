using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using DimensionsMod.DimensionsModCode.Cards.Basic;
using DimensionsMod.DimensionsModCode.Extensions;
using DimensionsMod.DimensionsModCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;

namespace DimensionsMod.DimensionsModCode.Character;

public class DimensionsMod : PlaceholderCharacterModel
{
    public const string CharacterId = "DimensionsMod";

    public static readonly Color Color = new("7030b0");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 65;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<GalacticStrike>(),
        ModelDb.Card<GalacticStrike>(),
        ModelDb.Card<GalacticStrike>(),
        ModelDb.Card<GalacticStrike>(),
        ModelDb.Card<GalacticStrike>(),
        ModelDb.Card<GalacticDefend>(),
        ModelDb.Card<GalacticDefend>(),
        ModelDb.Card<GalacticDefend>(),
        ModelDb.Card<GalacticDefend>(),
        ModelDb.Card<GalacticDefend>(),
        ModelDb.Card<PlasmaReserve>(),
        ModelDb.Card<CosmicDarkness>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<RedGiant>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<DimensionsModCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<DimensionsModRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<DimensionsModPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}