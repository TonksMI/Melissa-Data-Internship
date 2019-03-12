////////////////////////////////////////////////////////
///	Author			: Nam Huy Dinh Ngo
/// Company			: Melissa Data
///	File  			: BingLocationGeoCode.cs
///	Date  			: 2/3/2016
/// Email  			: nam@melissadata.com
////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GeoDataViewer.Models.Maps
{
    #region BING RESP
    public class BingGeoCodeAddress
    {
        public string addressLine { get; set; }
        public string adminDistrict { get; set; }
        public string adminDistrict2 { get; set; }
        public string countryRegion { get; set; }
        public string formattedAddress { get; set; }
        public string locality { get; set; }
        public string postalCode { get; set; }
    }
    public class BingGeoCodePoint
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
        public string calculationMethod { get; set; }
    }
    public class BingGeoCodeResource
    {
        public string name { get; set; }
        public BingGeoCodePoint point { get; set; }
        public BingGeoCodeAddress address { get; set; }
        public string confidence { get; set; }
        public string[] matchCodes { get; set; }
    }
    public class BingGeoCodeResourceSet
    {
        public int estimatedTotal { get; set; }
        public List<BingGeoCodeResource> resources { get; set; }
    }

    public class BingGeoCodeResponse
    {
        public string authenticationResultCode { get; set; }
        public string brandLogoUri { get; set; }
        public string copyright { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public string traceId { get; set; }
        public List<BingGeoCodeResourceSet> resourceSets { get; set; }
    }

    #endregion BING RESP

    public static class BingLocationsGeoCode
    {
        // basic developer key
        private static string _key = "";
        private static string _baseUrl = "http://dev.virtualearth.net/REST/v1/Locations";

        private static Dictionary<string, BingGeoCodeResponse> _bingDict;

        static BingLocationsGeoCode()
        {
            _bingDict = new Dictionary<string, BingGeoCodeResponse>();
        }
        public static async Task<BingGeoCodeResponse> DoLookupAsync(string address)
        {            
            return await Task.Run(() => {
                return DoLookup(address);
            });
        }
        public static BingGeoCodeResponse DoLookup(string address)
        {
            if (String.IsNullOrEmpty(address))
                return null;

            BingGeoCodeResponse _resp;

            if(_bingDict.TryGetValue(address, out _resp))
            {
                return _resp;
            }
            else
            {
                _resp = DoAddressLookup(address);
                _bingDict.Add(address, _resp);
                return _resp;
            }            
        }
        public static async Task<BingGeoCodeResponse> DoReverseLookupAsync(double Latitude, double Longitude)
        {
            return await Task.Run(() =>
            {
                BingGeoCodeResponse _resp;
                string latlongkey = String.Format("{0}_{1}", Latitude, Longitude);
                if (_bingDict.TryGetValue(latlongkey, out _resp))
                {
                    return _resp;
                }
                else
                {
                    _resp = DoReverseLookup(Latitude, Longitude);
                    _bingDict.Add(latlongkey, _resp);
                    return _resp;
                }
            });
        }
        public static BingGeoCodeResponse DoReverseLookup(string Latitude, string Longitude)
        {
            return Lookup(String.Format("/{0},{1}?", Latitude, Longitude));
        }
        public static BingGeoCodeResponse DoReverseLookup(double Latitude, double Longitude)
        {
            return Lookup(String.Format("/{0},{1}?",Latitude,Longitude));
        }
        private static BingGeoCodeResponse DoAddressLookup(string address)
        {
            return Lookup(String.Format("?q={0}", address));
        }
        private static BingGeoCodeResponse Lookup(string payload)
        {
            BingGeoCodeResponse resp;
            try
            {
                string request = String.Format("{0}{1}&key={2}",_baseUrl,payload,_key);

                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string response = client.DownloadString(request);
                // work around to solve the problem with __type field in JSON
                response = response.Replace("__", "");

                JavaScriptSerializer jss = new JavaScriptSerializer();
                resp = jss.Deserialize<BingGeoCodeResponse>(response);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return resp;
        }
    }
}
