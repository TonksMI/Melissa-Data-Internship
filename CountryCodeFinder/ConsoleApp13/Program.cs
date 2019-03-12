using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace ConsoleApp13
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connect = new SqlConnection("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI"))
            {
                //,[ADDRESSLINE2],[ADDRESSLINE3],[ADDRESSLINE4],[ADDRESSLINE5],[ADDRESSLINE6],[ADDRESSLINE7],[ADDRESSLINE8],[LOCALITY],[ADMINAREA],[POSTALCODE],[SUBADMINAREA] 
                connect.Open();
                SqlCommand getData = new SqlCommand("SELECT [RECID],[ADDRESSLINE1] FROM [InternWorkspace].[dbo].[NoCountry] WHERE LQCountryCode != NewCountryCode and LQCountryCode = 'HK'", connect);
                SqlDataReader data = getData.ExecuteReader();
                SqlConnection sql = new SqlConnection("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI");
                sql.Open();
                while (data.Read())
                {
                    String temp = "";
                    for (int i = 0; i < data.FieldCount; i++)
                    {
                        if (data.GetValue(i).ToString() != "")
                        {
                            temp += " " + data.GetValue(i);
                        }
                    }
                    try
                    {
                        SqlCommand insert = new SqlCommand($"UPDATE [InternWorkspace].[dbo].[NoCountry] SET [NewCountryCode] = '{FindCode(temp)}' WHERE [RECID] = {data.GetValue(0)}", sql);
                        insert.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }           
            Console.ReadLine();
        }

        public static string FindCode(string inputText)
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\Users\matthewt\Documents\CountryCodeNames.sqlite;Version=3;"))
            {
                connect.Open();
                string temp = "";
                char[] param = { ' ', ',' };
                string[] array = inputText.Split(param);
                int max = array.Length > 6 ? 6 : array.Length;
                for (int i = array.Length - 1; i >= array.Length - max; i--)
                {
                    if (temp == "")
                    {
                        temp = array[i].ToUpper();
                    }
                    else
                    {
                        temp = array[i].ToUpper() + " " + temp;
                    }
                    try
                    {
                        SQLiteCommand getValues = new SQLiteCommand($"SELECT CountryCode FROM CountryCodeNames WHERE CountryName = '{temp}'", connect);
                        using (SQLiteDataReader read = getValues.ExecuteReader())
                        {
                            read.Read();
                            String countryID = read.GetValue(0).ToString();
                            return countryID;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                return "";
            }
        }
    }
}
