using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace RESTCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLookUp_Click(object sender, EventArgs e)
        {
            //Sending REST request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RESTURL());
            try
            {
                //getting Response in the form of a JSON String
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                String jsonResponse = reader.ReadToEnd();
                reader.Close();
                try
                {
                    //Parseing the JSON String into a Useable Object
                    Result.Rootobject root = JsonConvert.DeserializeObject<Result.Rootobject>(jsonResponse);
                    Result.Record[] records = root.Records;

                    //Outputting                    
                    try
                    {
                        rchtxtREST.Clear();
                        rchtxtREST.AppendText(records[0].ToString());
                    }
                    catch (NullReferenceException)
                    {
                        //Catching Transmission Error code
                        rchtxtREST.AppendText("Error code: " + root.TransmissionResults + Environment.NewLine);
                        rchtxtREST.AppendText($"Please look at http://wiki.melissadata.com/index.php?title=Result_Code_Details#Global_Address_Verification for more information");
                    }
                    tabControl1.SelectedTab = tabREST;
                }
                catch (JsonException ex)
                {
                    //Output Error Message
                    rchtxtREST.AppendText(ex.Message + Environment.NewLine);
                }
            }
            catch (WebException ex)
            {
                //Output Error Message
                rchtxtREST.AppendText(ex.Message + Environment.NewLine);
            }
        }
        private String RESTURL()
        {
            //Format REST request
            String temp = $"https://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?&t={txtTransmission.Text}&id =" +
                $"{txtCustomerID.Text}&org={txtOrganization.Text}&a1={txtAddress1.Text}&a2={txtAddress2.Text}" +
                $"&a3={txtAddress3.Text}&a4={txtAddress4.Text}&a5={txtAddress5.Text}&a6={txtAddress6.Text}" +
                $"&a7={txtAddress7.Text}&a8={txtAddress8.Text}&ddeploc={txtDblDepLocality.Text}" +
                $"&deploc={txtDepLocality.Text}&loc={txtLocality.Text}&subadmarea={txtSubAdminArea.Text}" +
                $"&admarea={txtAdminArea.Text}&subnatarea={txtSubNationalArea}&postal={txtSubNationalArea.Text}" +
                $"&ctry={txtCountry.Text}&format=JSON";
            return temp;
        }
    }
}
