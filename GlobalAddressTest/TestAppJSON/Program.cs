using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;


namespace TestAppJSON
{
    class Program
    {
        static String CustomerID = "";
        static void Main(string[] args)
        {            
            List<Record> rec = new List<Record>();
            using (StreamReader read = new StreamReader(File.OpenRead(@"C:\Users\MatthewT\source\repos\TestAppJSON\1KGlobalTest.txt")))
            {
                String[] str = { "" };
                read.ReadLine();
                String test = read.ReadLine();
                do
                {
                    str = test.Split('|');
                    rec.Add(new Record(str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8],
                        str[9], str[10], str[11], str[13], str[14], str[15], str[16], str[17], str[18]));
                } while ((test = read.ReadLine()) != null);
            }

            // Makes the REST JSON Request and Outputs Responses to Console
            //MakeRESTRequest(rec.ToArray());
            //Console.WriteLine("End of REST request"+Environment.NewLine);
            //Console.ReadLine();

            //Makes the BATCH JSON Request and outputs responses to Console
            BatchRequest batch = new BatchRequest();
            BatchCall(rec.ToArray());
            Console.WriteLine("End of BATCH requests");
            Console.ReadLine();

        }


        public static String restRecord(RequestRecord rec, String customerID)
        {
            //Format REST request
            String temp = $"https://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?&id={customerID}&org={rec.Organization}&a1={rec.AddressLine1}&a2={rec.AddressLine2}" +
                $"&a3={rec.AddressLine3}&a4={rec.AddressLine4}&a5={rec.AddressLine5}&a6={rec.AddressLine6}" +
                $"&a7={rec.AddressLine7}&a8={rec.AddressLine8}&ddeploc={rec.DoubleDependentLocality}" +
                $"&deploc={rec.DependentLocality}&loc={rec.Locality}&subadmarea={rec.SubAdministrativeArea}" +
                $"&admarea={rec.AdministrativeArea}&subnatarea={rec.SubNationalArea}&postal={rec.PostalCode}" +
                $"&ctry={rec.CountryName}&format=JSON";
            return temp;
        }

        public static void MakeRESTRequest(Record[] records)
        {
            //Create Request
            HttpWebRequest request = null;
            RequestRecord[] reqrec = new RequestRecord[records.Length];
            for(int i = 0; i < records.Length; i++)
            {
                reqrec[i] = records[i].ToRequestRecord();
            }                
            foreach (RequestRecord rec in reqrec)
            {
                request = (HttpWebRequest)WebRequest.Create(restRecord(rec, ""));
                try
                {
                    //Get Response
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string jsonResponse = reader.ReadToEnd();

                    //Converts JSON to Usable object Result and outputs results
                    try
                    {
                        Result.Rootobject result = JsonConvert.DeserializeObject<Result.Rootobject>(jsonResponse);
                        foreach (Result.Record root in result.Records)
                        {
                            Console.WriteLine(root);
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                catch (WebException ex)
                {
                    //Output Error
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void BatchCall(Record[] records)
        {
            //Creates JSON batch to send to the webservice
            BatchRequest[] batch = new BatchRequest[records.Length / 100 + 1];
            
            //Splits the list into chunks of 100 and makes call
            Record[] temp = new Record[100];
            for (int i = 0; i < records.Length / 100; i++)
            {
                Array.ConstrainedCopy(records, i * 100, temp, 0, 100);
                batch[i] = new BatchRequest();
                batch[i].Records = temp;
                MakeBATCHJSONRequest(batch[i]);
            }
            //makes call with the leftover Records
            Array.Copy(records, records.Length / 100 * 100, temp.ToArray(), 0, records.Length % 100);
            batch[batch.Length - 1] = new BatchRequest();
            batch[batch.Length - 1].Records = temp;
            MakeBATCHJSONRequest(batch[batch.Length - 1]);
        }
        public static void MakeBATCHJSONRequest(BatchRequest batch)
        {
            //Makes the request to the webservice
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://address.melissadata.net/v3/WEB/GlobalAddress/doGlobalAddress");
                request.ContentType = $"text/json";
                request.Method = "POST";
                request.Accept = $"application/json";
                try
                {
                    using (StreamWriter write = new StreamWriter(request.GetRequestStream()))
                    {
                        write.Write(JsonConvert.SerializeObject(batch));
                    }
                    try
                    {
                        //Gets response
                        WebResponse response = request.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string jsonResponse = reader.ReadToEnd();
                        reader.Close();

                        //Converts JSON to Usable object Result.RootObject and outputs results
                        try
                        {                            
                            Result.Rootobject result = JsonConvert.DeserializeObject<Result.Rootobject>(jsonResponse);
                            if (result.Records != null)
                            {
                                foreach (Result.Record root in result.Records)
                                {
                                    Console.WriteLine(root);
                                }
                            }                            
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
