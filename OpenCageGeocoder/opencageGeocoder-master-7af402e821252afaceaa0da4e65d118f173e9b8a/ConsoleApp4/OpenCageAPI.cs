using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using Newtonsoft.Json;

namespace OpenCageAPI
{
    #region OPEN RESP

    public class OpenCageResults
    {
        public OpenCageAnnotations annotations { get; set; }
        public int confidence { get; set; }
        public string formatted { get; set; }
        public List<OpenCageGeometry> geometry { get; set; }
        public List<OpenCageComponents> components { get; set; }
        public List<OpenCageStatus> status { get; set; }
        public OpenCageRate rate { get; set; }

    }
    public class OpenCageAnnotations
    {
        public string DMS { get; set; }
        
    }
    public class OpenCageGeometry
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class OpenCageComponents
    {
        public string _type { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string road { get; set; }
        public string house_number { get; set; }
        public string state { get; set; }
        public string attraction { get; set; }

    }
    public class OpenCageRate
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }
    public class OpenCageStatus
    {
        public int code { get; set; }
        public string message { get; set; }
    }
    #endregion OPEN RESP

    public static class OpenCageGeoCode
    {
        private static string _key = "";
        private static string _baseUrl = "https://api.opencagedata.com/geocode/v1/json";

        private static Dictionary<string, OpenCageResults> _openDict;

        static OpenCageGeoCode()
        {
            _openDict = new Dictionary<string, OpenCageResults>();
        }

        public static async Task<OpenCageResults> DoLookUpAsync(string address)
        {
            return await Task.Run(() =>
            {
                return DoLookup(address);
            });
        }
        public static OpenCageResults DoLookup(string address)
        {
            if (String.IsNullOrEmpty(address))
            {
                return null;
            }

            OpenCageResults _resp;

            if(_openDict.TryGetValue(address, out _resp))
            {
                return _resp;
            }
            else
            {
                _resp = DoAddressLookup(address);
                _openDict.Add(address, _resp);
                return _resp;
            }
        }
        public static async Task<OpenCageResults> DoReverseLookupAsync(double latitude, double longitude)
        {
            return await Task.Run(() =>
            {
                OpenCageResults _resp;
                string latlongkey = String.Format("{0}_{1}", latitude, longitude);
                if (_openDict.TryGetValue(latlongkey, out _resp))
                {
                    return _resp;
                }
                else
                {
                    _resp = DoReverseLookup(latitude, longitude);
                    _openDict.Add(latlongkey, _resp);
                    return _resp;
                }
            });
        }
        public static OpenCageResults DoReverseLookup(string Latitude, string Longitude)
        {
            return Lookup(String.Format("?q={0}%C+{1}", Latitude, Longitude));
        }
        public static OpenCageResults DoReverseLookup(double Latitude, double Longitude)
        {
            return Lookup(String.Format("?q={0}%C+{1}", Latitude, Longitude));
        }
        private static OpenCageResults DoAddressLookup(string address)
        {
            return Lookup(String.Format("?q={0}", address));
        }
        private static OpenCageResults Lookup(string payload)
        {
            OpenCageResults resp;
            try
            {
                string request = String.Format("{0}{1}&key={2}&pretty=1&no_annotations=1", _baseUrl, payload, _key);

                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string response = client.DownloadString(request);
                response = response.Replace("__", "");
                //response = response.Replace(",", "+");

                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //resp = jss.Deserialize<OpenCageResults>(response);
                JsonObjectAttribute joa = new JsonObjectAttribute(response);
                resp = JsonConvert.DeserializeObject<OpenCageResults>(joa.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return resp;
        }
    }
}
