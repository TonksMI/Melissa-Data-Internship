using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace ConsoleApp4
{
    public class Rate
    {
        public int Limit { get; set; }
        public int Remaining { get; set; }
        public int Reset { get; set; }
    }
    public class License
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }   
    public class Location
    {        
        public Annotations Annotations { get; set; }        
        public string Formatted { get; set; }        
        public Dictionary<string, string> Components { get; set; }
        public AddressComponent ComponentAddress
        {
            get
            {
                return new AddressComponent
                {
                    BusStop = Components.GetValueOrDefault("bus_stop"),
                    City = Components.GetValueOrDefault("city"),
                    Country = Components.GetValueOrDefault("country"),
                    County = Components.GetValueOrDefault("county"),
                    CountryCode = Components.GetValueOrDefault("country_code"),
                    Postcode = Components.GetValueOrDefault("postcode"),
                    Road = Components.GetValueOrDefault("road"),
                    State = Components.GetValueOrDefault("state"),
                    StateDistrict = Components.GetValueOrDefault("state_district"),
                    Suburb = Components.GetValueOrDefault("suburb"),
                    Type = Components.GetValueOrDefault("_type")
                };
            }
        }        
        public Point Geometry { get; set; }        
        public Bounds Bounds { get; set; }        
        public int Confidence { get; set; }
    }
    
    public class AddressComponent
    {

        public string Type { get; set; }
        public string Country { get; set; }
        public string StateDistrict { get; set; }
        public string CountryCode { get; set; }
        public string State { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string BusStop { get; set; }
        public string County { get; set; }
        public string Road { get; set; }
        public string Postcode { get; set; }
    }
    public class Point
    {       
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class Annotations
    {
        public DMS DMS { get; set; }
        public string MGRS { get; set; }
        public string Maidenhead { get; set; }
        public Mercator Mercator { get; set; }
        public OSGB OSGB { get; set; }
        public OSM OSM { get; set; }
        public int CallingCode { get; set; }
        public Currency Currency { get; set; }
        public string Flag { get; set; }
        public string Geohash { get; set; }
        public double Qibla { get; set; }
        public Sun Sun { get; set; }
        public Timezone Timezone { get; set; }
        public What3words What3words { get; set; }
        public string Wikidata { get; set; }
    }
    public class What3words
    {
        public string Words { get; set; }
    }
    public class Timezone
    {
        public string Name { get; set; }
        public int NowInDst { get; set; }
        public int OffsetSec { get; set; }
        public int OffsetString { get; set; }
        public string ShortName { get; set; }
    }
    public class OSM
    {
        public string EditUrl { get; set; }
        public string Url { get; set; }
    }
    public class OSGB
    {
        public double Easting { get; set; }
        public string GridRef { get; set; }
        public double Northing { get; set; }
    }

    public class Set
    {
        public int Apparent { get; set; }
        public int Astronomical { get; set; }
        public int Civil { get; set; }
        public int Nautical { get; set; }
    }
    public class Rise
    {
        public int Apparent { get; set; }
        public int Astronomical { get; set; }
        public int Civil { get; set; }
        public int Nautical { get; set; }
    }
    public class Sun
    {
        public Rise Rise { get; set; }
        public Set Set { get; set; }
    }
    public class Currency
    {
        public string[] AlternateSymbols { get; set; }
        public string DecimalMark { get; set; }
        public string HtmlEntity { get; set; }
        public string IsoCode { get; set; }
        public int IsoNumeric { get; set; }
        public string Name { get; set; }
        public int SmallestDenomination { get; set; }
        public string Subunit { get; set; }
        public int SubunitToUnit { get; set; }
        public string Symbol { get; set; }
        public int SymbolFirst { get; set; }
        public string ThousandsSeparator { get; set; }
    }
    public class Bounds
    {
        public Point SouthWest { get; set; }

        public Point NorthEast { get; set; }
    }
    public class DMS
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
    public class Mercator
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
    public class RequestStatus
    {
        public string Message { get; set; }

        public int Code { get; set; }
    }
    public class Timestamp
    {
        public string CreatedHttp { get; set; }
        public int CreatedUnix { get; set; }
    }
    public class EchoRequest
    {
        public int Abbrv { get; set; }
        public int AddRequest { get; set; }
        public int AutoComplete { get; set; }
        public string CountryCode { get; set; }
        public string Format { get; set; }
        public string Key { get; set; }
        public string Language { get; set; }
        public int Limit { get; set; }
        public int MinConfidence { get; set; }
        public int NoAnnotations { get; set; }
        public int NoDedupe { get; set; }
        public int NoRecord { get; set; }
        public int OnlyNominatim { get; set; }
        public int Pretty { get; set; }
        public string Query { get; set; }
        public string Version { get; set; }
    }
}

