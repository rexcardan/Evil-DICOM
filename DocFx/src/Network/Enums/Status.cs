namespace EvilDICOM.Network.Enums
{
    //ps 3.7 Annex C Status Type Encoding(Normative)
    public enum Status : ushort
    {
        SUCCESS = 0,
        WARNING = 1,
        FAILURE = 10,
        FAILURE_UNABLE_TO_PROCESS=49152,
        FAILURE_UNABLE_TO_FIND = 49153,
        FAILURE_REFUSED=42752,
        FAILURE_ID_SOP = 43264,
        CANCEL = 65024,
        PENDING = 65280
    }
}