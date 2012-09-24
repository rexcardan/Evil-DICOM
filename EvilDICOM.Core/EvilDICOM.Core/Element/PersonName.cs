using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.Element
{
    public class PersonName : AbstractElement<string>
    {
        public string Data
        {
            get { return base.Data; }
            set { base.Data = DataRestriction.EnforceLengthRestriction(64, value); }
        }

        public PersonName() { }

        public PersonName(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
        }

        public string FirstName
        {
            get { return PersonNameHelper.GetFirstName(Data); }
            set { Data = PersonNameHelper.SetFirstName(Data, value); }
        }

        public string MiddleName
        {
            get { return PersonNameHelper.GetMiddleName(Data); }
            set { Data = PersonNameHelper.SetMiddleName(Data, value); }
        }

        public string LastName
        {
            get { return PersonNameHelper.GetLastName(Data); }
            set { Data = PersonNameHelper.SetLastName(Data, value); }
        }
    }
}