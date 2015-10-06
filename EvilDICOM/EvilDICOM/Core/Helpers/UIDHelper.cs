using System;
using System.Linq;

//using System.Numerics;

namespace EvilDICOM.Core.Helpers
{
    public class UIDHelper
    {
        public static string GenerateUID()
        {
            return string.Format("{0}.{1}", "2.25", GenerateSystemGuid());
        }

        public static string GenerateUID(string prefix)
        {
            return string.Format("{0}.{1}", prefix, GenerateSystemGuid());
        }

        public static string GenerateUID(string countryCode, string vendorID, string suffix)
        {
            return string.Format("{0}.{1}.{2}.{3}", "2.16", countryCode, vendorID, suffix);
        }

        //.NET 4.0 Implementation (will wait until adopted on more computers)       
        //private static BigInteger GenerateSystemGuid()
        //{
        //    Guid g = Guid.NewGuid();
        //    BigInteger b = new BigInteger(g.ToByteArray());
        //    return b < 0 ? b * -1 : b;
        //}

        private static string GenerateSystemGuid()
        {
            Guid g = Guid.NewGuid();
            var uints = new uint[4];
            Buffer.BlockCopy(g.ToByteArray(), 0, uints, 0, 16);
            return String.Join("", uints.Select(i => i.ToString()).ToArray());
        }
    }

    public static class ISO3166Helper
    {
        public static string AFGHANISTAN = "4";
        public static string ALAND_ISLANDS = "248";
        public static string ALBANIA = "8";
        public static string ALGERIA = "12";
        public static string AMERICAN_SAMOA = "16";
        public static string ANDORRA = "20";
        public static string ANGOLA = "24";
        public static string ANGUILLA = "660";
        public static string ANTARCTICA = "10";
        public static string ANTIGUA_AND_BARBUDA = "28";
        public static string ARGENTINA = "32";
        public static string ARMENIA = "51";
        public static string ARUBA = "533";
        public static string AUSTRALIA = "36";
        public static string AUSTRIA = "40";
        public static string AZERBAIJAN = "31";
        public static string BAHAMAS = "44";
        public static string BAHRAIN = "48";
        public static string BANGLADESH = "50";
        public static string BARBADOS = "52";
        public static string BELARUS = "112";
        public static string BELGIUM = "56";
        public static string BELIZE = "84";
        public static string BENIN = "204";
        public static string BERMUDA = "60";
        public static string BHUTAN = "64";
        public static string BOLIVIA_PLURINATIONAL_STATE_OF = "68";
        public static string BONAIRE_SINT_EUSTATIUS_AND_SABA = "535";
        public static string BOSNIA_AND_HERZEGOVINA = "70";
        public static string BOTSWANA = "72";
        public static string BOUVET_ISLAND = "74";
        public static string BRAZIL = "76";
        public static string BRITISH_INDIAN_OCEAN_TERRITORY = "86";
        public static string BRUNEI_DARUSSALAM = "96";
        public static string BULGARIA = "100";
        public static string BURKINA_FASO = "854";
        public static string BURUNDI = "108";
        public static string CAMBODIA = "116";
        public static string CAMEROON = "120";
        public static string CANADA = "124";
        public static string CAPE_VERDE = "132";
        public static string CAYMAN_ISLANDS = "136";
        public static string CENTRAL_AFRICAN_REPUBLIC = "140";
        public static string CHAD = "148";
        public static string CHILE = "152";
        public static string CHINA = "156";
        public static string CHRISTMAS_ISLAND = "162";
        public static string COCOS_KEELING_ISLANDS = "166";
        public static string COLOMBIA = "170";
        public static string COMOROS = "174";
        public static string CONGO = "178";
        public static string CONGO_THE_DEMOCRATIC_REPUBLIC_OF_THE = "180";
        public static string COOK_ISLANDS = "184";
        public static string COSTA_RICA = "188";
        public static string COTE_DIVOIRE = "384";
        public static string CROATIA = "191";
        public static string CUBA = "192";
        public static string CURACAO = "531";
        public static string CYPRUS = "196";
        public static string CZECH_REPUBLIC = "203";
        public static string DENMARK = "208";
        public static string DJIBOUTI = "262";
        public static string DOMINICA = "212";
        public static string DOMINICAN_REPUBLIC = "214";
        public static string ECUADOR = "218";
        public static string EGYPT = "818";
        public static string EL_SALVADOR = "222";
        public static string EQUATORIAL_GUINEA = "226";
        public static string ERITREA = "232";
        public static string ESTONIA = "233";
        public static string ETHIOPIA = "231";
        public static string FALKLAND_ISLANDS_MALVINAS = "238";
        public static string FAROE_ISLANDS = "234";
        public static string FIJI = "242";
        public static string FINLAND = "246";
        public static string FRANCE = "250";
        public static string FRENCH_GUIANA = "254";
        public static string FRENCH_POLYNESIA = "258";
        public static string FRENCH_SOUTHERN_TERRITORIES = "260";
        public static string GABON = "266";
        public static string GAMBIA = "270";
        public static string GEORGIA = "268";
        public static string GERMANY = "276";
        public static string GHANA = "288";
        public static string GIBRALTAR = "292";
        public static string GREECE = "300";
        public static string GREENLAND = "304";
        public static string GRENADA = "308";
        public static string GUADELOUPE = "312";
        public static string GUAM = "316";
        public static string GUATEMALA = "320";
        public static string GUERNSEY = "831";
        public static string GUINEA = "324";
        public static string GUINEA_BISSAU = "624";
        public static string GUYANA = "328";
        public static string HAITI = "332";
        public static string HEARD_ISLAND_AND_MCDONALD_ISLANDS = "334";
        public static string HOLY_SEE_VATICAN_CITY_STATE = "336";
        public static string HONDURAS = "340";
        public static string HONG_KONG = "344";
        public static string HUNGARY = "348";
        public static string ICELAND = "352";
        public static string INDIA = "356";
        public static string INDONESIA = "360";
        public static string IRAN_ISLAMIC_REPUBLIC_OF = "364";
        public static string IRAQ = "368";
        public static string IRELAND = "372";
        public static string ISLE_OF_MAN = "833";
        public static string ISRAEL = "376";
        public static string ITALY = "380";
        public static string JAMAICA = "388";
        public static string JAPAN = "392";
        public static string JERSEY = "832";
        public static string JORDAN = "400";
        public static string KAZAKHSTAN = "398";
        public static string KENYA = "404";
        public static string KIRIBATI = "296";
        public static string KOREA_DEMOCRATIC_PEOPLES_REPUBLIC_OF = "408";
        public static string KOREA_REPUBLIC_OF = "410";
        public static string KUWAIT = "414";
        public static string KYRGYZSTAN = "417";
        public static string LAO_PEOPLES_DEMOCRATIC_REPUBLIC = "418";
        public static string LATVIA = "428";
        public static string LEBANON = "422";
        public static string LESOTHO = "426";
        public static string LIBERIA = "430";
        public static string LIBYAN_ARAB_JAMAHIRIYA = "434";
        public static string LIECHTENSTEIN = "438";
        public static string LITHUANIA = "440";
        public static string LUXEMBOURG = "442";
        public static string MACAO = "446";
        public static string MACEDONIA_THE_FORMER_YUGOSLAV_REPUBLIC_OF = "807";
        public static string MADAGASCAR = "450";
        public static string MALAWI = "454";
        public static string MALAYSIA = "458";
        public static string MALDIVES = "462";
        public static string MALI = "466";
        public static string MALTA = "470";
        public static string MARSHALL_ISLANDS = "584";
        public static string MARTINIQUE = "474";
        public static string MAURITANIA = "478";
        public static string MAURITIUS = "480";
        public static string MAYOTTE = "175";
        public static string MEXICO = "484";
        public static string MICRONESIA_FEDERATED_STATES_OF = "583";
        public static string MOLDOVA_REPUBLIC_OF = "498";
        public static string MONACO = "492";
        public static string MONGOLIA = "496";
        public static string MONTENEGRO = "499";
        public static string MONTSERRAT = "500";
        public static string MOROCCO = "504";
        public static string MOZAMBIQUE = "508";
        public static string MYANMAR = "104";
        public static string NAMIBIA = "516";
        public static string NAURU = "520";
        public static string NEPAL = "524";
        public static string NETHERLANDS = "528";
        public static string NEW_CALEDONIA = "540";
        public static string NEW_ZEALAND = "554";
        public static string NICARAGUA = "558";
        public static string NIGER = "562";
        public static string NIGERIA = "566";
        public static string NIUE = "570";
        public static string NORFOLK_ISLAND = "574";
        public static string NORTHERN_MARIANA_ISLANDS = "580";
        public static string NORWAY = "578";
        public static string OMAN = "512";
        public static string PAKISTAN = "586";
        public static string PALAU = "585";
        public static string PALESTINIAN_TERRITORY_OCCUPIED = "275";
        public static string PANAMA = "591";
        public static string PAPUA_NEW_GUINEA = "598";
        public static string PARAGUAY = "600";
        public static string PERU = "604";
        public static string PHILIPPINES = "608";
        public static string PITCAIRN = "612";
        public static string POLAND = "616";
        public static string PORTUGAL = "620";
        public static string PUERTO_RICO = "630";
        public static string QATAR = "634";
        public static string REUNION = "638";
        public static string ROMANIA = "642";
        public static string RUSSIAN_FEDERATION = "643";
        public static string RWANDA = "646";
        public static string SAINT_BARTHELEMY = "652";
        public static string SAINT_HELENA_ASCENSION_AND_TRISTAN_DA_CUNHA = "654";
        public static string SAINT_KITTS_AND_NEVIS = "659";
        public static string SAINT_LUCIA = "662";
        public static string SAINT_MARTIN_FRENCH_PART = "663";
        public static string SAINT_PIERRE_AND_MIQUELON = "666";
        public static string SAINT_VINCENT_AND_THE_GRENADINES = "670";
        public static string SAMOA = "882";
        public static string SAN_MARINO = "674";
        public static string SAO_TOME_AND_PRINCIPE = "678";
        public static string SAUDI_ARABIA = "682";
        public static string SENEGAL = "686";
        public static string SERBIA = "688";
        public static string SEYCHELLES = "690";
        public static string SIERRA_LEONE = "694";
        public static string SINGAPORE = "702";
        public static string SINT_MAARTEN_DUTCH_PART = "534";
        public static string SLOVAKIA = "703";
        public static string SLOVENIA = "705";
        public static string SOLOMON_ISLANDS = "90";
        public static string SOMALIA = "706";
        public static string SOUTH_AFRICA = "710";
        public static string SOUTH_GEORGIA_AND_THE_SOUTH_SANDWICH_ISLANDS = "239";
        public static string SOUTH_SUDAN = "728";
        public static string SPAIN = "724";
        public static string SRI_LANKA = "144";
        public static string SUDAN = "729";
        public static string SURINAME = "740";
        public static string SVALBARD_AND_JAN_MAYEN = "744";
        public static string SWAZILAND = "748";
        public static string SWEDEN = "752";
        public static string SWITZERLAND = "756";
        public static string SYRIAN_ARAB_REPUBLIC = "760";
        public static string TAIWAN_PROVINCE_OF_CHINA = "158";
        public static string TAJIKISTAN = "762";
        public static string TANZANIA_UNITED_REPUBLIC_OF = "834";
        public static string THAILAND = "764";
        public static string TIMOR_LESTE = "626";
        public static string TOGO = "768";
        public static string TOKELAU = "772";
        public static string TONGA = "776";
        public static string TRINIDAD_AND_TOBAGO = "780";
        public static string TUNISIA = "788";
        public static string TURKEY = "792";
        public static string TURKMENISTAN = "795";
        public static string TURKS_AND_CAICOS_ISLANDS = "796";
        public static string TUVALU = "798";
        public static string UGANDA = "800";
        public static string UKRAINE = "804";
        public static string UNITED_ARAB_EMIRATES = "784";
        public static string UNITED_KINGDOM = "826";
        public static string UNITED_STATES = "840";
        public static string UNITED_STATES_MINOR_OUTLYING_ISLANDS = "581";
        public static string URUGUAY = "858";
        public static string UZBEKISTAN = "860";
        public static string VANUATU = "548";
        public static string VATICAN_CITY_STATE_HOLY_SEE = "336";
        public static string VENEZUELA_BOLIVARIAN_REPUBLIC_OF = "862";
        public static string VIET_NAM = "704";
        public static string VIRGIN_ISLANDS_BRITISH = "92";
        public static string VIRGIN_ISLANDS_US = "850";
        public static string WALLIS_AND_FUTUNA = "876";
        public static string WESTERN_SAHARA = "732";
        public static string YEMEN = "887";
        public static string YUGOSLAVIA = "891";
        public static string ZAMBIA = "894";
        public static string ZIMBABWE = "716";
    }
}