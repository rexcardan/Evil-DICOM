#region

using EvilDICOM.Core;

#endregion

namespace EvilDICOM.Anonymization
{
    public interface IAnonymizer
    {
        void Anonymize(DICOMObject d);
    }
}