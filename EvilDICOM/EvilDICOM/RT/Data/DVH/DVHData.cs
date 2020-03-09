namespace EvilDICOM.RT.Data.DVH
{
    public class DVHData
    {
        public DVHPoint[] Points { get; set; }
        public Dose MaxDose { get; set; }
        public Dose MinDose { get; set; }
        public Dose MeanDose { get; set; }

        public double VolumeCC { get; set; }
    }
}