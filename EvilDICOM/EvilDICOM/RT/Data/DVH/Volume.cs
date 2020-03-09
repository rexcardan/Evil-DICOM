namespace EvilDICOM.RT.Data.DVH
{
    public class Volume
    {
        public Volume(double value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        public double Value { get; }
        public string Unit { get; }
    }
}