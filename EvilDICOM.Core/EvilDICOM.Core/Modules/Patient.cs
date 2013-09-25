using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.Modules
{
    public class Patient : IIOD
    {
        private PersonName _name = new PersonName { Tag = TagHelper.PATIENT_NAME };
        private LongString _id = new LongString { Tag = TagHelper.PATIENT_ID };
        private Date _birthdate = new Date { Tag = TagHelper.PATIENT_BIRTH_DATE };
        private CodeString _sex = new CodeString { Tag = TagHelper.PATIENT_SEX };

        public string Name
        {
            get
            {
                return _name.Data;
            }
            set
            {
                _name.Data = value;
            }
        }

        public string ID
        {
            get
            {
                return _id.Data;
            }
            set
            {
                _id.Data = value;
            }
        }

        public System.DateTime? BirthDate
        {
            get
            {
                return _birthdate.DataContainer.SingleValue;
            }
            set
            {
                _birthdate.DataContainer.SingleValue = value;
            }
        }

        public string Sex
        {
            get
            {
                return _sex.Data;
            }
            set
            {
                _sex.Data = value;
            }
        }

        public List<IDICOMElement> Elements
        {
            get
            {
                return new List<IDICOMElement>() { _name, _id, _birthdate, _sex };
            }
        }
    }
}
