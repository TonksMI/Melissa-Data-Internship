using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoDataViewer.Models.Maps;
using System.Threading.Tasks;

namespace BingMapAPIUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task BingMapAddressLookup()
        {
            BingGeoCodeResponse resp = await BingLocationsGeoCode.DoLookupAsync(@"22382 Avenida Empresa Rancho Santa Margarita, CA 92688");
            if(resp!=null && resp.resourceSets !=null && resp.resourceSets.Count >0)
            {
                if (resp.resourceSets[0] != null && resp.resourceSets[0].resources.Count > 0)
                {
                    if(resp.resourceSets[0].resources[0] != null)
                    {

                        string lat = resp.resourceSets[0].resources[0].point.coordinates[0].ToString();
                        string lon = resp.resourceSets[0].resources[0].point.coordinates[1].ToString();
                        Console.WriteLine("Lat: {0}, Lon: {1}", lat, lon);
                    }
                }
            }
        }

        [TestMethod]
        public async Task BingMapReverseGeoLookup()
        {
            BingGeoCodeResponse resp = await BingLocationsGeoCode.DoReverseLookupAsync(33.63755, -117.60684);
            if (resp != null && resp.resourceSets != null && resp.resourceSets.Count > 0)
            {
                if (resp.resourceSets[0] != null && resp.resourceSets[0].resources.Count > 0)
                {
                    if (resp.resourceSets[0].resources[0] != null)
                    {
                        string bingAddress = resp.resourceSets[0].resources[0].address.addressLine;
                        string bingCity = resp.resourceSets[0].resources[0].address.locality;
                        string bingZip = resp.resourceSets[0].resources[0].address.postalCode;
                        string bingState = resp.resourceSets[0].resources[0].address.adminDistrict;
                        Console.WriteLine("Address: {0} {1} {2}, {3} ", bingAddress, bingCity, bingState,bingZip);
                    }
                }
            }
        }
    }
}
