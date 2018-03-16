namespace EvilDICOM.Network.Enums
{
    public enum CommandField : ushort
    {
        C_STORE_RQ = 1,
        C_ECHO_RQ = 48,
        C_FIND_RQ = 32,
        C_MOVE_RQ = 33,
        C_GET_RQ = 16,
        C_GET_RP = 32784,
        C_FIND_RP = 32800,
        C_MOVE_RP = 32801,
        C_ECHO_RP = 32816,
        C_STORE_RP = 32769,
        C_CANCEL = 4095,
        N_ACTION_RQ = 304,
        N_ACTION_RP = 8130
    }
}