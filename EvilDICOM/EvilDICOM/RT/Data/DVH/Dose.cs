namespace EvilDICOM.RT.Data.DVH
{
    public class Dose
    {
        public Dose(double value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        public double Value { get; }
        public string Unit { get; }
    }
}