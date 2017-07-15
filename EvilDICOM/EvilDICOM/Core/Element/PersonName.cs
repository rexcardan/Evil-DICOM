using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the PersonName VR type
    /// </summary>
    public class PersonName : AbstractElement<string>
    {
        public PersonName()
        {
            VR = VR.PersonName;
        }

        public PersonName(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.PersonName;
        }

        public override string Data
        {
            get { return base.DataContainer.SingleValue; }
            set
            {
                base.DataContainer = base.DataContainer ?? new DICOMData<string>();
                base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(64, value);
            }
        }

        /// <summary>
        ///     A property to help get and set the first name of the person name string.
        /// </summary>
        public string FirstName
        {
            get { return PersonNameHelper.GetFirstName(Data); }
            set { Data = PersonNameHelper.SetFirstName(Data, value); }
        }

        /// <summary>
        ///     A property to help get and set the middle name of the person name string.
        /// </summary>
        public string MiddleName
        {
            get { return PersonNameHelper.GetMiddleName(Data); }
            set { Data = PersonNameHelper.SetMiddleName(Data, value); }
        }

        /// <summary>
        ///     A property to help get and set the last name of the person name string.
        /// </summary>
        public string LastName
        {
            get { return PersonNameHelper.GetLastName(Data); }
            set { Data = PersonNameHelper.SetLastName(Data, value); }
        }
    }
}