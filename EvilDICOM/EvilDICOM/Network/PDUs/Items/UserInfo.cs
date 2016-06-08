using System.Text;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Network.PDUs.Items
{
    /// <summary>
    /// Class UserInfo.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfo"/> class.
        /// </summary>
        public UserInfo()
        {
            //Defaults
            MaxPDULength = 16384;
            ImplementationUID = Constants.EVIL_DICOM_IMP_UID;
            ImplementationVersion = Constants.EVIL_DICOM_IMP_VERSION;
        }

        /// <summary>
        /// Gets or sets the maximum length of the pdu.
        /// </summary>
        /// <value>The maximum length of the pdu.</value>
        public int MaxPDULength { get; set; }
        /// <summary>
        /// Gets or sets the implementation uid.
        /// </summary>
        /// <value>The implementation uid.</value>
        public string ImplementationUID { get; set; }
        /// <summary>
        /// Gets or sets the asynchronous operations.
        /// </summary>
        /// <value>The asynchronous operations.</value>
        public AsyncOperations AsynchronousOperations { get; set; }
        /// <summary>
        /// Gets or sets the implementation version.
        /// </summary>
        /// <value>The implementation version.</value>
        public string ImplementationVersion { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
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