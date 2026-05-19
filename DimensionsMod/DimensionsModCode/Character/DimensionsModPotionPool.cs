using BaseLib.Abstracts;
using DimensionsMod.DimensionsModCode.Extensions;
using Godot;

namespace DimensionsMod.DimensionsModCode.Character;

public class DimensionsModPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => DimensionsMod.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}