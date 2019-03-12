using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TestAppJSON
{
    public class RequestRecord
    {
        public RequestRecord()
        {

        }
        public RequestRecord(String AddressLine1, String AddressLine2, String AddressLine3, String AddressLine4, String AddrressLine5, String AddressLine6,
            String AddressLine7, String AddressLine8, String Locality, String AdministrativeArea, String PostalCode, String CountryName, String CountryCode,
            String DependentLocality, String DoubleDependentLocality, String SubAdministrativeArea, String SubNationalArea, String Organization, String RecordID)
        {
            this.AddressLine1 = AddressLine1;
            this.AddressLine2 = AddressLine2;
            this.AddressLine3 = AddressLine3;
            this.AddressLine4 = AddressLine4;
            this.AddressLine5 = AddressLine5;
            this.AddressLine6 = AddressLine6;
            this.AddressLine7 = AddressLine7;
            this.AddressLine8 = AddressLine8;
            this.Locality = Locality;
            this.AdministrativeArea = AdministrativeArea;
            this.PostalCode = PostalCode;
            this.CountryName = CountryName;
            this.CountryISO3166_1_Alpha2 = CountryCode;
            this.DependentLocality = DependentLocality;
            this.DoubleDependentLocality = DoubleDependentLocality;
            this.SubAdministrativeArea = SubAdministrativeArea;
            this.SubNationalArea = SubNationalArea;
            this.Organization = Organization;
            this.RecordID = RecordID;
        }
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
        public Record ToRecord()
        {
            Record reqrec = new Record();
            reqrec.AddressLine1 = this.AddressLine1;
            reqrec.AddressLine2 = this.AddressLine2;
            reqrec.AddressLine3 = this.AddressLine3;
            reqrec.AddressLine4 = this.AddressLine4;
            reqrec.AddressLine5 = this.AddressLine5;
            reqrec.AddressLine6 = this.AddressLine6;
            reqrec.AddressLine7 = this.AddressLine7;
            reqrec.AddressLine8 = this.AddressLine8;
            reqrec.Locality = this.Locality;
            reqrec.AdministrativeArea = this.AdministrativeArea;
            reqrec.PostalCode = this.PostalCode;
            reqrec.Country = this.CountryName;
            reqrec.DependentLocality = this.DependentLocality;
            reqrec.DoubleDependentLocality = this.DoubleDependentLocality;
            reqrec.SubAdministrativeArea = this.SubAdministrativeArea;
            reqrec.SubNationalArea = this.SubNationalArea;
            reqrec.Organization = this.Organization;
            reqrec.RecordID = this.RecordID;
            return reqrec;
        }
    }
}
