using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.VR;
using EvilDicom.Helper;

namespace EvilDicom.RTPlan
{
    public class ControlPoint
    {
        SequenceItem si;

        public ControlPoint(SequenceItem si)
        {
            if (si.Find(TagHelper.CONTROL_POINT_INDEX) != null)
            {
                this.si = si;
                BeamLimitingDevicePosSequence test = this.BeamLimitingDeviceSequence;
            }
        }

        public int ControlPointIndex
        {
            get
            {
                IntegerString cp = si.Find(TagHelper.CONTROL_POINT_INDEX) as IntegerString;
                return cp.Data[0];
            }
            set
            {
                IntegerString cp = si.Find(TagHelper.CONTROL_POINT_INDEX) as IntegerString;
                cp.Data = new int[] { value };
            }
        }

        public double GantryAngle
        {
            get
            {
                DecimalString ga = si.Find(TagHelper.GANTRY_ANGLE) as DecimalString;
                return ga.Data[0];
            }
            set
            {
                DecimalString ga = si.Find(TagHelper.GANTRY_ANGLE) as DecimalString;
                ga.Data = new double[] { value };
            }

        }

        public string GantryRotationDirection
        {
            get
            {
                CodeString gd = si.Find(TagHelper.GANTRY_ROTATION_DIRECTION) as CodeString;
                return gd.Data;
            }
            set
            {
                CodeString gd = si.Find(TagHelper.GANTRY_ROTATION_DIRECTION) as CodeString;
                gd.Data = value;
            }

        }

        public BeamLimitingDevicePosSequence BeamLimitingDeviceSequence
        {
            get
            {
                Sequence s = si.Find(TagHelper.BEAM_LIMITING_DEVICE_POSITION_SEQUENCE) as Sequence;
                BeamLimitingDevicePosSequence bldp = new BeamLimitingDevicePosSequence(s);
                return bldp;
            }

            set
            {
                Sequence s = si.Find(TagHelper.BEAM_LIMITING_DEVICE_POSITION_SEQUENCE) as Sequence;
                s.Items = value.asSequence().Items;
            }

        }

    }
}


//Copyright © 2012 Rex Cardan, Ph.D


