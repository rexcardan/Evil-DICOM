#region

using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.Element;

#endregion

namespace EvilDICOM.Core.Selection
{
    public partial class SequenceSelector : AbstractElement<DICOMSelector>
    {
        public SequenceSelector(Sequence s)
        {
            Data_ = new List<DICOMSelector>();
            foreach (var item in s.Items)
                Data_.Add(new DICOMSelector(item));
            Tag = s.Tag;
        }

        public List<DICOMSelector> Items
        {
            get { return Data_; }
        }

        public Sequence ToSequence()
        {
            var s = new Sequence();
            s.Tag = Tag;
            foreach (var item in Items)
                s.Items.Add(item.ToDICOMObject());
            return s;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} {2}", Tag, Enums.VR.Sequence, string.Format(" : {0} Items", Items.Count));
        }
    }
}