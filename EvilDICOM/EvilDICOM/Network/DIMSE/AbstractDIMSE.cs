using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.Enums;
using C = EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    public abstract class AbstractDIMSE : AbstractDIMSEBase
    {
        private DICOMObject _data;

        public bool HasData
        {
            get
            {
                return DataSetType != 257; //0x01 0x01
            }
        }

        public DICOMObject Data
        {
            get { return _data; }
            set
            {
                _data = value;
                DataSetType = value != null ? (ushort)0 : (ushort)257;
            }
        }


        //TODO Move this to the association class
        internal int DataPresentationContextId { get; set; }

    }
}