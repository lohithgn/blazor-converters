using System;
using System.Linq;
using UnitsNet;
using UnitsNet.Units;

namespace BlazorConverters.Client.Services
{
    public class UnitsService
    {
        public UnitCategory GetUnitCategory(string category)
        {
            UnitCategory unitCategory;
            var parsed = Enum.TryParse(category, true, out unitCategory);
            if(!parsed)
            {
                unitCategory = UnitCategory.Unknown;
            }
            return unitCategory;
        }

        public string[] GetUnits(UnitCategory category)
        {
            string[] units = null;
            switch(category)
            {
                case UnitCategory.Volume:
                    units = Enum.GetNames(typeof(VolumeUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Length:
                    units = Enum.GetNames(typeof(LengthUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Mass:
                    units = Enum.GetNames(typeof(MassUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Temperature:
                    units = Enum.GetNames(typeof(TemperatureUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Energy:
                    units = Enum.GetNames(typeof(EnergyUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Area:
                    units = Enum.GetNames(typeof(AreaUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Speed:
                    units = Enum.GetNames(typeof(SpeedUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Power:
                    units = Enum.GetNames(typeof(PowerUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Pressure:
                    units = Enum.GetNames(typeof(PressureUnit)).Skip(1).ToArray();
                    break;
                case UnitCategory.Angle:
                    units = Enum.GetNames(typeof(AngleUnit)).Skip(1).ToArray();
                    break;
            }
            return units;
        }

        public double Convert(int value, UnitCategory category, string sourceUnit, string targetUnit)
        {
            return UnitConverter.ConvertByName(value, category.ToString(), sourceUnit, targetUnit);
        }
    }
}
