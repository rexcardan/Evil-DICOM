using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the ApplicationEntity VR type
    /// </summary>
    public class ApplicationEntity : AbstractElement<string>
    {
        public ApplicationEntity() : base() { VR = Enums.VR.ApplicationEntity; }

        public ApplicationEntity(Tag tag, string data)
            : base(tag,data)
        {
            VR = Enums.VR.ApplicationEntity;
        }
    }
}
