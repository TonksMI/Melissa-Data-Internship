using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)

        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI");


            using (SqlConnection sql = new SqlConnection(builder.ConnectionString))
            {
                sql.Open();
                SqlCommand myCommand = new SqlCommand("SELECT TOP(1000) [MAK],[Address],[City],[State] FROM [InternWorkspace].[dbo].[MasterAddressUS];", sql);
                SqlConnection connect = new SqlConnection("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI");
                connect.Open();
                SqlDataReader reader = myCommand.ExecuteReader();
                List<object> fail = new List<object>();
                int count = 1;
                while (reader.Read())
                {                 

                    SqlCommand uploadData = new SqlCommand();
                    var gc = new Geocoder("");


                    //  example with lots of optional parameters
                    var result2 = gc.Geocode($"{ reader.GetValue(1)} { reader.GetValue(2)} { reader.GetValue(3)}", countrycode: "", limit: 1, minConfidence: 10, language: "en", abbrv: true, noAnnotations: false, noRecord: false, addRequest: true);
                    //var result2 = gc.Geocode("", limit: 1, minConfidence: 10, language: "en", abbrv: false, noAnnotations: true, noRecord: true, addRequest: true);
                    //result2.PrintDump();
                    try
                    {

                        String[] temp = result2.Results[0].Formatted.Split(',');
                        
                        if (temp.Length > 4)
                        {
                            uploadData = new SqlCommand($"INSERT INTO [InternWorkspace].[dbo].[OpenCageResults10]VALUES({reader.GetValue(0)},'{temp[0].Replace("'","")}','{temp[temp.Length - 3]}','{temp[temp.Length - 1]}',{result2.Results[0].Geometry.lat},{result2.Results[0].Geometry.lng})", connect);
                        }
                        else
                        {
                            uploadData = new SqlCommand($"INSERT INTO [InternWorkspace].[dbo].[OpenCageResults10]VALUES({reader.GetValue(0)},'{temp[0].Replace("'", " ")}','{temp[temp.Length - 3]}','{temp[temp.Length - 1]}',{result2.Results[0].Geometry.lat},{result2.Results[0].Geometry.lng})", connect);
                        }
                        uploadData.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Fail "+count);
                        count++;
                    }

                    // var reserveresult = gc.ReverseGeocode(51.4277844, -0.3336517);

                    //reserveresult.PrintDump();               

                }                
            }
        }
    }
}
