using System.Text;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Network.PDUs.Items
{
    public class UserInfo
    {
        public UserInfo()
        {
            //Defaults
            MaxPDULength = 16384;
            ImplementationUID = Constants.EVIL_DICOM_IMP_UID;
            ImplementationVersion = Constants.EVIL_DICOM_IMP_VERSION;
        }

        public int MaxPDULength { get; set; }
        public string ImplementationUID { get; set; }
        public AsyncOperations AsynchronousOperations { get; set; }
        public string ImplementationVersion { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("USER INFO");
            sb.AppendLine("-------------");
            sb.AppendLine(string.Format("Max PDU Length : {0}", MaxPDULength));
            sb.AppendLine(string.Format("Implementation UID : {0}", ImplementationUID));
            sb.AppendLine(string.Format("Implementation Version : {0}", ImplementationVersion));
            if (AsynchronousOperations != null) sb.AppendLine(AsynchronousOperations.ToString());
            return sb.ToString();
        }
    }
}