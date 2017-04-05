using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the AttributeTag VR type
    /// </summary>
    public class AttributeTag : AbstractElement<Tag>
    {
        public AttributeTag()
        {
            VR = VR.AttributeTag;
        }

        public AttributeTag(Tag tag, Tag data)
            : base(tag, data)
        {
            VR = VR.AttributeTag;
        }
    }
}