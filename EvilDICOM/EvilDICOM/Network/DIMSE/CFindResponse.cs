using EvilDICOM.Core;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.DIMSE.IOD;
using System.Collections.Generic;
using C = EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    /// <summary>
    /// Class CFindResponse.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.DIMSE.AbstractDIMSEResponse" />
    public class CFindResponse : AbstractDIMSEResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CFindResponse"/> class.
        /// </summary>
        /// <param name="d">The d.</param>
        public CFindResponse(DICOMObject d)
            : base(d)
        {
            CommandField = (ushort) C.C_FIND_RQ;
        }

        /// <summary>
        /// Gets the iod.
        /// </summary>
        /// <returns>CFindIOD.</returns>
        public CFindIOD GetIOD()
        {
            if (this.Data != null)
            {
                return new CFindIOD(new DICOMObject(this.Data.Elements));
            }
            return null;
        }
    }
}