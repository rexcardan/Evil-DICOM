using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using EvilDicom.VR;
using EvilDicom.Helper;
using EvilDicom.Image;

namespace EvilDicom.RTPlan
{
    public class DoseCube
    {

        private string doseUnit;
        private ScalingFunction doseGridScaling;

        public DoseCube() { }
        //public DoseCube(DicomFile df)
        //{
        //    init(df);
        //    base.init(df);
            
        //}

        public string DoseUnit
        {
            get { return doseUnit; }
            set { doseUnit = value; }
        }
        public ScalingFunction DoseGridScaling
        {
            get { return doseGridScaling; }
            set { doseGridScaling = value; }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


