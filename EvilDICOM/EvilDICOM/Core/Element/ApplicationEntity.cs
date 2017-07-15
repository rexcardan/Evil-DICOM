using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the ApplicationEntity VR type
    /// </summary>
    public class ApplicationEntity : AbstractElement<string>
    {
        public ApplicationEntity()
        {
            VR = VR.ApplicationEntity;
        }

        public ApplicationEntity(Tag tag, string data)
            : base(tag, data)
        {
            VR = VR.ApplicationEntity;
        }
    }
}