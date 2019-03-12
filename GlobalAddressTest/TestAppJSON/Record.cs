using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppJSON
{
    public class Record
    {

        public Record()
        {

        }
        public Record(String AddressLine1, String AddressLine2, String AddressLine3, String AddressLine4, String AddressLine5, String AddressLine6,
            String AddressLine7, String AddressLine8, String Locality, String AdministrativeArea, String PostalCode, String CountryName,
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
            this.Country = CountryName;
            this.DependentLocality = DependentLocality;
            this.DoubleDependentLocality = DoubleDependentLocality;
            this.SubAdministrativeArea = SubAdministrativeArea;
            this.SubNationalArea = SubNationalArea;
            this.Organization = Organization;
            this.RecordID = RecordID;
        }
        public string RecordID { get; set; }
        public string Organization { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string AddressLine6 { get; set; }
        public string AddressLine7 { get; set; }
        public string AddressLine8 { get; set; }
        public string DoubleDependentLocality { get; set; }
        public string DependentLocality { get; set; }
        public string Locality { get; set; }
        public string SubAdministrativeArea { get; set; }
        public string AdministrativeArea { get; set; }
        public string PostalCode { get; set; }
        public string SubNationalArea { get; set; }
        public string Country { get; set; }
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
        public RequestRecord ToRequestRecord()
        {
            return new RequestRecord(AddressLine1, AddressLine2, AddressLine3, AddressLine4, AddressLine5, AddressLine6,
             AddressLine7, AddressLine8, Locality, AdministrativeArea, PostalCode, Country, "",
             DependentLocality, DoubleDependentLocality, SubAdministrativeArea, SubNationalArea, Organization, RecordID);
        }

    }
}
