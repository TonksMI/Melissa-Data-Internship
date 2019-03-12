using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoDataViewer.Models.Maps;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace BingMapAPI
{
    class Program
    {

        static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI");


            using (SqlConnection sql = new SqlConnection(builder.ConnectionString))
            {
                sql.Open();
                SqlCommand myCommand = new SqlCommand("SELECT [MAK],[Address],[City],[State] FROM [InternWorkspace].[dbo].[BingRequest];", sql);
                SqlConnection connect = new SqlConnection("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI");
                connect.Open();
                SqlDataReader reader = myCommand.ExecuteReader();
                List<object> fail = new List<object>();
                while (reader.Read())
                {

                    SqlCommand uploadData = new SqlCommand();
                    BingGeoCodeResponse response = BingLocationsGeoCode.DoLookup($"{reader.GetValue(1)} {reader.GetValue(2)} {reader.GetValue(3)}");

                    try
                    {

                        String temp = "";
                        foreach (string str in response.resourceSets[0].resources[0].matchCodes)
                        {
                            temp += str + "|";
                        }
                        temp = temp.TrimEnd('|');
                        if (response.resourceSets[0].resources[0].address.addressLine == null)
                        {
                            uploadData = new SqlCommand($"INSERT INTO [InternWorkspace].[dbo].[BingResultsPart2]([MAK],[Address],[City],[Country],[Latitude],[Longitude],[Confidence]," +
                                $"[Match Codes],[County],[Method]) VALUES ({reader.GetValue(0)}, '','{response.resourceSets[0].resources[0].address.locality.Replace("'", " ")}'," +
                                $"'{response.resourceSets[0].resources[0].address.adminDistrict.Replace("'", " ")}'" +
                                $",{response.resourceSets[0].resources[0].point.coordinates[0]},{response.resourceSets[0].resources[0].point.coordinates[1]},'{response.resourceSets[0].resources[0].confidence.Replace("'", " ")}'" +
                                $",'{temp}','{response.resourceSets[0].resources[0].address.adminDistrict2.Replace("'", " ")}','Unknown');", connect);
                        }
                        else
                        {
                            uploadData = new SqlCommand($"INSERT INTO [InternWorkspace].[dbo].[BingResultsPart2]([MAK],[Address],[City],[Country],[Latitude],[Longitude],[Confidence]," +
                                $"[Match Codes],[County],[Method]) VALUES ({reader.GetValue(0)},' {response.resourceSets[0].resources[0].address.addressLine.Replace("'", " ")}','{response.resourceSets[0].resources[0].address.locality.Replace("'", " ")}'" +
                                $",'{response.resourceSets[0].resources[0].address.adminDistrict.Replace("'", " ")}'," +
                                $"{response.resourceSets[0].resources[0].point.coordinates[0]},{response.resourceSets[0].resources[0].point.coordinates[1]},'{response.resourceSets[0].resources[0].confidence.Replace("'", " ")}','{temp}','{response.resourceSets[0].resources[0].address.adminDistrict2.Replace("'", " ")}','Unknown');", connect);
                        }

                        uploadData.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        fail.Add(reader.GetValue(0));
                    }
                    using (StreamWriter write = new StreamWriter("faillog.txt"))
                    {
                        foreach (object integer in fail)
                        {
                            write.WriteLine(integer.ToString());
                        }
                    }
                }
            }
             using (StreamReader read = new StreamReader(@"C:\Users\matthewt\source\repos\BingMapAPI\BingMapAPI\bin\Debug\faillog.txt"))
            {
                String temp = "";
                while ((temp = read.ReadLine()) != null)
                {
                    string strConnection = "SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI";
                    SqlConnection myConnection = new SqlConnection(strConnection);
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand($"INSERT INTO FailedBingRequests SELECT * FROM MasterAddressUS WHERE MAK = {temp}", myConnection);
                    SqlDataReader ignore = myCommand.ExecuteReader();
                    myConnection.Close();
                }
            }
            //string strConnection = "SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI";
            //SqlConnection myConnection = new SqlConnection(strConnection);
            //myConnection.Open();

            //SqlCommand myCommand = new SqlCommand("SELECT * FROM [InternWorkspace].[dbo].[BingRequest] order by MAK", myConnection);
            //SqlDataReader myDataReader = myCommand.ExecuteReader();
            //List<object> fail = new List<object>();
            //List<SqlCommand> commands = new List<SqlCommand>();
            //DataColumn MAK = new DataColumn("MAK", typeof(int));
            //DataColumn Address = new DataColumn("Address", typeof(string));
            //DataColumn City = new DataColumn("City", typeof(string));
            //DataColumn County = new DataColumn("County", typeof(string));
            //DataColumn Country = new DataColumn("Country", typeof(string));
            //DataColumn Latitude = new DataColumn("Latitude", typeof(double));
            //DataColumn Longitude = new DataColumn("Longitude", typeof(double));
            //DataColumn Confidence = new DataColumn("Confidence", typeof(string));
            //DataColumn MatchCodes = new DataColumn("Match Codes", typeof(string));          
            //DataColumn Method = new DataColumn("Method", typeof(string));
            //DataTable table = new DataTable();
            //table.Columns.Add(MAK);
            //table.Columns.Add(Address);
            //table.Columns.Add(City);
            //table.Columns.Add(Country);
            //table.Columns.Add(Latitude);
            //table.Columns.Add(Longitude);
            //table.Columns.Add(Confidence);
            //table.Columns.Add(MatchCodes);
            //table.Columns.Add(County);
            //table.Columns.Add(Method);
            //while (myDataReader.Read())
            //{
            //    BingGeoCodeResponse response = BingLocationsGeoCode.DoLookup($"{myDataReader.GetValue(2)} {myDataReader.GetValue(3)} {myDataReader.GetValue(5)} {myDataReader.GetValue(6)}");
            //    string temp = "";
            //    try
            //    {
            //        foreach (string str in response.resourceSets[0].resources[0].matchCodes)
            //        {
            //            temp += str + "|";
            //        }
            //        temp = temp.TrimEnd('|');
            //        DataRow data = table.NewRow();
            //        data[MAK] = myDataReader.GetValue(0);
            //        data[Address] = response.resourceSets[0].resources[0].address.addressLine;
            //        data[City] = response.resourceSets[0].resources[0].address.locality;
            //        data[County] = response.resourceSets[0].resources[0].address.adminDistrict2;
            //        data[Country] = response.resourceSets[0].resources[0].address.adminDistrict;
            //        data[Latitude] = response.resourceSets[0].resources[0].point.coordinates[0];
            //        data[Longitude] = response.resourceSets[0].resources[0].point.coordinates[1];
            //        data[Confidence] = response.resourceSets[0].resources[0].confidence;
            //        data[MatchCodes] = temp;
            //        table.Rows.Add(data);
            //    }
            //    catch (Exception)
            //    {
            //        fail.Add(myDataReader.GetValue(0));
            //    }
            //}
            //using (StreamWriter write = new StreamWriter("faillog.txt"))
            //{
            //    foreach (object integer in fail)
            //    {
            //        write.WriteLine(integer.ToString());
            //    }
            //}
            //SqlBulkCopy bulk = new SqlBulkCopy(myConnection);
            //myDataReader.Close();
            //bulk.DestinationTableName = "[InternWorkspace].[dbo].[BingResults]";
            //bulk.WriteToServer(table);
            //myConnection.Close();
        }
    }
}
