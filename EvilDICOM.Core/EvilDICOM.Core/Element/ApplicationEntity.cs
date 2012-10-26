using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class ApplicationEntity : AbstractElement<string>
    {
        public ApplicationEntity() { }

        public ApplicationEntity(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.ApplicationEntity;
        }
    }
}
