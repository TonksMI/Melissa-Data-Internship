using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net;
using System.IO;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connect = new SqlConnection("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI"))
            {
                connect.Open();
                SqlCommand data = new SqlCommand("SELECT [MAK], [Address], [City], [State],[CensusKey] FROM [InternWorkspace].[dbo].[MasterAddressUS]", connect);
                using (SqlDataReader read = data.ExecuteReader())
                {
                    while (read.Read())
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Rest(read.GetValue(1).ToString(), read.GetValue(2).ToString(), read.GetValue(3).ToString()));
                        try
                        {
                            WebResponse response = request.GetResponse();
                            String jsonResponse = "";
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                jsonResponse = reader.ReadToEnd();
                                jsonResponse = jsonResponse.Replace("Census Blocks", "CensusBlocks");                                
                            }
                            try
                            {
                                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
                                using (SqlConnection send = new SqlConnection("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI"))
                                {
                                    send.Open();
                                    try
                                    {
                                        SqlCommand sendData = new SqlCommand($"INSERT INTO [dbo].[Comparison]([MAK],[OurCensusKey],[TheirGeoID])VALUES({read.GetValue(0)},'{read.GetValue(4).ToString()}','{root.result.addressMatches[0].geographies.CensusBlocks[0].GEOID}')", send);
                                        sendData.ExecuteNonQuery();
                                    }
                                    catch(Exception)
                                    {
                                        SqlCommand sendData = new SqlCommand($"INSERT INTO [dbo].[Comparison]([MAK],[OurCensusKey],[TheirGeoID])VALUES({read.GetValue(0)},'{read.GetValue(4).ToString()}',0)", send);
                                        sendData.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (JsonException)
                            {
                                Console.WriteLine("Json failed");
                            }
                        }
                        catch (WebException)
                        {
                            Console.WriteLine("Web failed");
                        }
                    }
                }
            }
        }
        public static String Rest(String Address, String City, String State)
        {
            return $"https://geocoding.geo.census.gov/geocoder/geographies/address?" +
                $"street={Address}&city={City}&state={State}&benchmark=Public_AR_Census2010" +
                $"&vintage=Census2010_Census2010&layers=14&format=json";
        }


        public class Rootobject
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public Input input { get; set; }
            public Addressmatch[] addressMatches { get; set; }
        }

        public class Input
        {
            public Address address { get; set; }
            public Benchmark benchmark { get; set; }
            public Vintage vintage { get; set; }
        }

        public class Address
        {
            public string state { get; set; }
            public string city { get; set; }
            public string street { get; set; }
        }

        public class Benchmark
        {
            public string id { get; set; }
            public bool isDefault { get; set; }
            public string benchmarkDescription { get; set; }
            public string benchmarkName { get; set; }
        }

        public class Vintage
        {
            public string id { get; set; }
            public bool isDefault { get; set; }
            public string vintageName { get; set; }
            public string vintageDescription { get; set; }
        }

        public class Addressmatch
        {
            public Geographies geographies { get; set; }
            public string matchedAddress { get; set; }
            public Coordinates coordinates { get; set; }
            public Tigerline tigerLine { get; set; }
            public Addresscomponents addressComponents { get; set; }
        }

        public class Geographies
        {
            public CensusBlock[] CensusBlocks { get; set; }
        }

        public class CensusBlock
        {
            public string BLKGRP { get; set; }
            public string UR { get; set; }
            public long OID { get; set; }
            public string FUNCSTAT { get; set; }
            public string STATE { get; set; }
            public int AREAWATER { get; set; }
            public string NAME { get; set; }
            public string SUFFIX { get; set; }
            public string LSADC { get; set; }
            public string CENTLON { get; set; }
            public int HU100 { get; set; }
            public string LWBLKTYP { get; set; }
            public string BLOCK { get; set; }
            public string BASENAME { get; set; }
            public string INTPTLAT { get; set; }
            public int POP100 { get; set; }
            public string MTFCC { get; set; }
            public string COUNTY { get; set; }
            public string GEOID { get; set; }
            public string CENTLAT { get; set; }
            public string INTPTLON { get; set; }
            public int AREALAND { get; set; }
            public int OBJECTID { get; set; }
            public string TRACT { get; set; }
        }

        public class Coordinates
        {
            public float x { get; set; }
            public float y { get; set; }
        }

        public class Tigerline
        {
            public string tigerLineId { get; set; }
            public string side { get; set; }
        }

        public class Addresscomponents
        {
            public string state { get; set; }
            public string zip { get; set; }
            public string city { get; set; }
            public string fromAddress { get; set; }
            public string toAddress { get; set; }
            public string preQualifier { get; set; }
            public string preDirection { get; set; }
            public string preType { get; set; }
            public string streetName { get; set; }
            public string suffixType { get; set; }
            public string suffixDirection { get; set; }
            public string suffixQualifier { get; set; }
        }
    }
}
