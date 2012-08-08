using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.VR;
using EvilDicom.Helper;

namespace EvilDicom.RTPlan
{
    public class BeamLimitingDevicePosSequence
    {
        public Sequence s;
        public BeamLimitingDevicePosSequence(Sequence s)
        {
            if (s.Tag.Id == TagHelper.BEAM_LIMITING_DEVICE_POSITION_SEQUENCE)
            {
                this.s = s;
            }
        }

        public BeamLimitingDevicePosition[] BeamLimitingDevicePositions
        {
            get
            {
                List<SequenceItem> items = s.Items;
                BeamLimitingDevicePosition[] positions = new BeamLimitingDevicePosition[items.Count];

                for (int i = 0; i < items.Count; i++)
                {
                    positions[i] = new BeamLimitingDevicePosition(items.ElementAt(i));
                }

                return positions;
            }
            set {
                List<SequenceItem> items = s.Items;
                for (int i = 0; i < value.Length; i++)
                {
                    items.Add(value[i].asSequenceItem());
                }
                s.Items = items ;
            }
        }

        public Sequence asSequence() {
            return this.s;
        }

    }
}


//Copyright © 2012 Rex Cardan, Ph.D


