using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the AttributeTag VR type
    /// </summary>
    /// <seealso cref="EvilDICOM.Core.Element.AbstractElement{EvilDICOM.Core.Element.Tag}" />
    public class AttributeTag : AbstractElement<Tag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeTag"/> class.
        /// </summary>
        public AttributeTag()
        {
            VR = VR.AttributeTag;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeTag"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public AttributeTag(Tag tag, Tag data)
            : base(tag, data)
        {
            VR = VR.AttributeTag;
        }
    }
}