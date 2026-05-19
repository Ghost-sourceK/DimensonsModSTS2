using BaseLib.Abstracts;
using BaseLib.Utils;
using DimensionsMod.DimensionsModCode.Character;

namespace DimensionsMod.DimensionsModCode.Potions;

[Pool(typeof(DimensionsModPotionPool))]
public abstract class DimensionsModPotion : CustomPotionModel;