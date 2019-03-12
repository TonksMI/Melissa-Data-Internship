using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppJSON
{
    public class Result
    {

        public class Rootobject
        {
            public string Version { get; set; }
            public string TransmissionReference { get; set; }
            public string TransmissionResults { get; set; }
            public string TotalRecords { get; set; }
            public Record[] Records { get; set; }
        }

        public class Record
        {
            public string RecordID { get; set; }
            public string Results { get; set; }
            public string FormattedAddress { get; set; }
            public string Organization { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string AddressLine3 { get; set; }
            public string AddressLine4 { get; set; }
            public string AddressLine5 { get; set; }
            public string AddressLine6 { get; set; }
            public string AddressLine7 { get; set; }
            public string AddressLine8 { get; set; }
            public string SubPremises { get; set; }
            public string DoubleDependentLocality { get; set; }
            public string DependentLocality { get; set; }
            public string Locality { get; set; }
            public string SubAdministrativeArea { get; set; }
            public string AdministrativeArea { get; set; }
            public string PostalCode { get; set; }
            public string AddressType { get; set; }
            public string AddressKey { get; set; }
            public string SubNationalArea { get; set; }
            public string CountryName { get; set; }
            public string CountryISO3166_1_Alpha2 { get; set; }
            public string CountryISO3166_1_Alpha3 { get; set; }
            public string CountryISO3166_1_Numeric { get; set; }
            public string Thoroughfare { get; set; }
            public string ThoroughfarePreDirection { get; set; }
            public string ThoroughfareLeadingType { get; set; }
            public string ThoroughfareName { get; set; }
            public string ThoroughfareTrailingType { get; set; }
            public string ThoroughfarePostDirection { get; set; }
            public string DependentThoroughfare { get; set; }
            public string DependentThoroughfarePreDirection { get; set; }
            public string DependentThoroughfareLeadingType { get; set; }
            public string DependentThoroughfareName { get; set; }
            public string DependentThoroughfareTrailingType { get; set; }
            public string DependentThoroughfarePostDirection { get; set; }
            public string Building { get; set; }
            public string PremisesType { get; set; }
            public string PremisesNumber { get; set; }
            public string SubPremisesType { get; set; }
            public string SubPremisesNumber { get; set; }
            public string PostBox { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public override string ToString()
            {
                if (Organization != "")
                {
                    return Organization + "" + AddressLine1 + " " + Locality + " " + AdministrativeArea + " " + PostalCode;
                }
                else
                {
                    return AddressLine1 + " " + Locality + " " + AdministrativeArea + " " + PostalCode;
                }
            }
        }
    }
}
