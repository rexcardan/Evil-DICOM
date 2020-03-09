namespace EvilDICOM.RT.Data.DVH
{
    public class DVHPoint
    {
        public DVHPoint(Dose dose, Volume volume)
        {
            Dose = dose;
            Volume = volume;
        }

        public Dose Dose { get; set; }
        public Volume Volume { get; set; }
    }
}