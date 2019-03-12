using System;

namespace RESTCSharp
{
    public class Result
    {
        //The JSON Response class found on http://wiki.melissadata.com/index.php?title=Global_Address_Verification%3AREST_JSON
        //Use "Edit => Paste Special => Paste JSON as Classes"
        //This creates the Classes below
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
                return "Address Line 1: " + AddressLine1 + Environment.NewLine +
                    "Address Line 2: " + AddressLine2 + Environment.NewLine +
                    "Address Line 3: " + AddressLine3 + Environment.NewLine +
                    "Address Line 4: " + AddressLine4 + Environment.NewLine +
                    "Address Line 5: " + AddressLine5 + Environment.NewLine +
                    "Address Line 6: " + AddressLine6 + Environment.NewLine +
                    "Address Line 7: " + AddressLine7 + Environment.NewLine +
                    "Address Line 8: " + AddressLine8 + Environment.NewLine +
                    "SubPremises: " + SubPremises + Environment.NewLine +
                    "Double Dependent Locality: " + DoubleDependentLocality + Environment.NewLine +
                    "Dependent Locality: " + DependentLocality + Environment.NewLine +
                    "Locality: " + Locality + Environment.NewLine +
                    "SubAdministraion Area: " + SubAdministrativeArea + Environment.NewLine +
                    "Administrative Area: " + AdministrativeArea + Environment.NewLine +
                    "Postal Code" + PostalCode + Environment.NewLine +
                    "Address Type: " + AddressType + Environment.NewLine +
                    "Address Key: " + AddressKey + Environment.NewLine +
                    "Sub National Area: " + SubNationalArea + Environment.NewLine +
                    "Country Name: " + CountryName + Environment.NewLine +
                    "Country ISO3166_!_Alpha2: " + CountryISO3166_1_Alpha2 + Environment.NewLine +
                    "CountryISO3166_1_Alpha3: " + CountryISO3166_1_Alpha3 + Environment.NewLine +
                    "CountryISO3166_1_Numeric: " + CountryISO3166_1_Numeric + Environment.NewLine +
                    "Thorough Fare: " + Thoroughfare + Environment.NewLine +
                    "ThoroughFare Predirection: " + ThoroughfarePreDirection + Environment.NewLine +
                    "Thoroughfare Leading Type: " + ThoroughfareLeadingType + Environment.NewLine +
                    "Thoroughfare name: " + ThoroughfareName + Environment.NewLine +
                    "Thoroughfare PostDirection: " + ThoroughfarePostDirection + Environment.NewLine +
                    "Dependent Thoroughfare: " + DependentThoroughfare + Environment.NewLine +
                    "Dependent Thoroughfare Predirection: " + DependentThoroughfarePreDirection + Environment.NewLine +
                    "Dependent Thoroughfare Leading Type: " + DependentThoroughfareLeadingType + Environment.NewLine +
                    "Dependent Thoroughfare Name: " + DependentThoroughfareName + Environment.NewLine +
                    "Dependent Thoroughfare Trailing Type: " + DependentThoroughfareTrailingType + Environment.NewLine +
                    "Depengent Thoroughfare Post Direction: " + DependentThoroughfarePostDirection + Environment.NewLine +
                    "Building: " + Building + Environment.NewLine +
                    "Premises Type: " + PremisesType + Environment.NewLine +
                    "Premises Number: " + PremisesNumber + Environment.NewLine +
                    "Subpremises Type: " + SubPremisesType + Environment.NewLine +
                    "Subpremises Number: " + SubPremisesNumber + Environment.NewLine +
                    "Postbox: " + PostBox + Environment.NewLine +
                    "Latitude: " + Latitude + Environment.NewLine +
                    "Longitude: " + Longitude;
            }
        }
    }
}
