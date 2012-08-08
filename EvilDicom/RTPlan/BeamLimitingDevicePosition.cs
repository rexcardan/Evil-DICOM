using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.VR;
using EvilDicom.Helper;

namespace EvilDicom.RTPlan
{
    public class BeamLimitingDevicePosition
    {
        SequenceItem si;
        public BeamLimitingDevicePosition(SequenceItem si)
        {
            if (si.Find(TagHelper.RTBEAM_LIMITING_DEVICE_TYPE) != null)
            {
                this.si = si;
            }
        }

        public BeamLimitingDevicePosition() {
            si = new SequenceItem();

            //Add blank RTBeamDeviceType
            CodeString cs = new CodeString();
            cs.Tag.Id = TagHelper.RTBEAM_LIMITING_DEVICE_TYPE;
            si.AddObject(cs);

            //Add blank Leaf Jaw Positions
            DecimalString ds = new DecimalString();
            ds.Tag.Id = TagHelper.LEAF_JAW_POSITIONS;
            si.AddObject(ds);

        }

        public string RtBeamLimitingDeviceType
        {
            get
            {
                CodeString cs = si.Find(TagHelper.RTBEAM_LIMITING_DEVICE_TYPE) as CodeString;
                return cs.Data;
            }
            set
            {
                CodeString cs = si.Find(TagHelper.RTBEAM_LIMITING_DEVICE_TYPE) as CodeString;
                cs.Data = value;
            }
        }

        public double[] LeafJawPositions
        {
            get
            {
                DecimalString ds = si.Find(TagHelper.LEAF_JAW_POSITIONS) as DecimalString;
                return ds.Data;
            }
            set
            {
                DecimalString ds = si.Find(TagHelper.LEAF_JAW_POSITIONS) as DecimalString;
                ds.Data = value;
            }

        }

        public SequenceItem asSequenceItem()
        {
            return this.si;
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


