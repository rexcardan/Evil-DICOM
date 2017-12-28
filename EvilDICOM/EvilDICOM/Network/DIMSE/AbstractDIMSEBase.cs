#region

using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Writing;

#endregion

namespace EvilDICOM.Network.DIMSE
{
    public abstract class AbstractDIMSEBase : IIOD
    {
        protected UniqueIdentifier _affectedSOPClassUID = new UniqueIdentifier {Tag = TagHelper.AffectedSOPClassUID};
        protected UnsignedShort _commandField = new UnsignedShort {Tag = TagHelper.CommandField, Data = 1};
        protected UnsignedShort _dataSetType = new UnsignedShort {Tag = TagHelper.CommandDataSetType};
        protected UnsignedLong _groupLength = new UnsignedLong {Tag = TagHelper.CommandGroupLength};

        public string AffectedSOPClassUID
        {
            get { return _affectedSOPClassUID.Data; }
            set { _affectedSOPClassUID.Data = value; }
        }

        public uint GroupLength
        {
            get { return _groupLength.Data; }
            set { _groupLength.Data = value; }
        }

        public ushort CommandField
        {
            get { return _commandField.Data; }
            protected set { _commandField.Data = value; }
        }

        public ushort DataSetType
        {
            get { return _dataSetType.Data; }
            set { _dataSetType.Data = value; }
        }

        public abstract List<IDICOMElement> Elements { get; }

        public void SetGroupLength()
        {
            var bytes = GroupWriter.WriteGroupBytes(new DICOMObject(Elements), DICOMIOSettings.Default(), "0000");
            GroupLength = (uint) bytes.Length;
        }
    }
}