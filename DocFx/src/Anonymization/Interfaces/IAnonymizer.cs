using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Anonymization
{
    public interface IAnonymizer
    {
        void Anonymize(DICOMObject d);
    }
}
