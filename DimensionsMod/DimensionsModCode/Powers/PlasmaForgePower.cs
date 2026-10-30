using DimensionsMod.DimensionsModCode.Abstract;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace DimensionsMod.DimensionsModCode.Powers;

public sealed class PlasmaForgePower : DimensionsModPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;
}