#region

using System.Collections.Generic;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using DateTime = System.DateTime;

#endregion

namespace EvilDICOM.Core.Modules
{
    public class Patient : IIOD
    {
        private readonly Date _birthdate = new Date {Tag = TagHelper.Patient​Birth​Date};
        private readonly LongString _id = new LongString {Tag = TagHelper.Patient​ID};
        private readonly PersonName _name = new PersonName {Tag = TagHelper.Patient​Name};
        private readonly CodeString _sex = new CodeString {Tag = TagHelper.Patient​Sex};

        public string Name
        {
            get { return _name.Data; }
            set { _name.Data = value; }
        }

        public string ID
        {
            get { return _id.Data; }
            set { _id.Data = value; }
        }

        public DateTime? BirthDate
        {
            get { return _birthdate.DataContainer.SingleValue; }
            set { _birthdate.DataContainer.SingleValue = value; }
        }

        public string Sex
        {
            get { return _sex.Data; }
            set { _sex.Data = value; }
        }

        public List<IDICOMElement> Elements
        {
            get { return new List<IDICOMElement> {_name, _id, _birthdate, _sex}; }
        }
    }
}