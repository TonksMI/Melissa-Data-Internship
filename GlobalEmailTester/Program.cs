using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.Data;
using System.ComponentModel;
using System.Net.Mail;

namespace ConsoleApp15
{
    class Program
    {
        public static DataTable ip;

        public static string serviceipPath;
        public static string emailfilePath;
        public static string lastFileused;
        public static string lastFileCache;
        public static string date;
        //= @"C:\Users\matthewt\Desktop\ServiceIPs.txt"
        //= @"C:\Users\matthewt\Desktop\EmailList.txt"
        // = "90fv0qperdfpwdofwdfas87"
        // = "OsOJYIr4qKPXEd4Nf4jyMY**"
        public static string internalLicense;
        public static string customerLicense;
        //"90fv0qperdfpwdofwdfas87"
        public static List<string> testEmails;
        public static List<string> recieveEmails;
        public static string recieveEmail;
        public static string passlog;
        public static string failedlog;
        public static int failedCount;
        public static int index = 0;
        public static string savepath;
        public static string test;
        static void Main(string[] args)
        {
            initializeConfig();
            InitializeIPTable();
            InitiallizeLogFileName();
            StartTest();
        }
        //Saves the values from the config file into the program
        public static void initializeConfig()
        {
            using (StreamReader read = new StreamReader("config.txt"))
            {
                internalLicense = read.ReadLine().Split('-')[1];
                customerLicense = read.ReadLine().Split('-')[1];
                emailfilePath = read.ReadLine().Split('-')[1];
                serviceipPath = read.ReadLine().Split('-')[1];
                recieveEmail = read.ReadLine().Split('-')[1];
                savepath = read.ReadLine().Split('-')[1];
                test = read.ReadLine().Split('-')[1];
                lastFileCache = read.ReadLine().Split('-')[1];
            }
        }
        //Initializes the Service table
        public static void InitializeIPTable()
        {
            ip = new DataTable();
            DataColumn locations = new DataColumn("Location", typeof(string));
            DataColumn machine = new DataColumn("Service", typeof(string));
            DataColumn service = new DataColumn("Machine", typeof(string));
            DataColumn IP = new DataColumn("IP", typeof(string));
            DataColumn expected = new DataColumn("Expected", typeof(string));
            DataColumn Active = new DataColumn("Active", typeof(int));
            ip.Columns.Add(locations);
            ip.Columns.Add(machine);
            ip.Columns.Add(service);
            ip.Columns.Add(IP);
            ip.Columns.Add(expected);
            ip.Columns.Add(Active);
            using (StreamReader read = new StreamReader(File.OpenRead(serviceipPath)))
            {
                read.ReadLine();
                string temp = "";
                while ((temp = read.ReadLine()) != null)
                {
                    string[] rows = temp.Split('\t');
                    //Uses the Location in the config file to only add from that locaiton into the IP table
                    if (test == "ALL")
                    {
                        DataRow row = ip.NewRow();
                        row["Location"] = rows[0].Replace("\"", "");
                        row["Service"] = rows[1].Replace("\"", "");
                        row["Machine"] = rows[2].Replace("\"", "");
                        row["IP"] = rows[3].Replace("\"", "");
                        row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                        row["Active"] = rows[5].Replace("\"", "");
                        ip.Rows.Add(row);
                    }
                    else if (test == "WEST")
                    {
                        if (rows[0].Replace("\"", "") == "west")
                        {
                            DataRow row = ip.NewRow();
                            row["Location"] = rows[0].Replace("\"", "");
                            row["Service"] = rows[1].Replace("\"", "");
                            row["Machine"] = rows[2].Replace("\"", "");
                            row["IP"] = rows[3].Replace("\"", "");
                            row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                            row["Active"] = rows[5].Replace("\"", "");
                            ip.Rows.Add(row);
                        }
                    }
                    else if (test == "EAST")
                    {
                        if (rows[0].Replace("\"", "") == "east")
                        {
                            DataRow row = ip.NewRow();
                            row["Location"] = rows[0].Replace("\"", "");
                            row["Service"] = rows[1].Replace("\"", "");
                            row["Machine"] = rows[2].Replace("\"", "");
                            row["IP"] = rows[3].Replace("\"", "");
                            row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                            row["Active"] = rows[5].Replace("\"", "");
                            ip.Rows.Add(row);
                        }
                    }
                    else if (test == "GLOBALEMAIL")
                    {
                        if (rows[1].Replace("\"", "") == "globalemail.melissadata.net")
                        {
                            DataRow row = ip.NewRow();
                            row["Location"] = rows[0].Replace("\"", "");
                            row["Service"] = rows[1].Replace("\"", "");
                            row["Machine"] = rows[2].Replace("\"", "");
                            row["IP"] = rows[3].Replace("\"", "");
                            row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                            row["Active"] = rows[5].Replace("\"", "");
                            ip.Rows.Add(row);
                        }
                    }
                    else if (test == "PROXYEMAILROUTER")
                    {
                        if (rows[1].Replace("\"", "") == "proxyemailrouterrc.melissadata.com")
                        {
                            DataRow row = ip.NewRow();
                            row["Location"] = rows[0].Replace("\"", "");
                            row["Service"] = rows[1].Replace("\"", "");
                            row["Machine"] = rows[2].Replace("\"", "");
                            row["IP"] = rows[3].Replace("\"", "");
                            row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                            row["Active"] = rows[5].Replace("\"", "");
                            ip.Rows.Add(row);
                        }
                    }
                    else if (test == "EBPEMAILROUTER")
                    {
                        if (rows[1].Replace("\"", "") == "ebpemailrouter.melissadata.com")
                        {
                            DataRow row = ip.NewRow();
                            row["Location"] = rows[0].Replace("\"", "");
                            row["Service"] = rows[1].Replace("\"", "");
                            row["Machine"] = rows[2].Replace("\"", "");
                            row["IP"] = rows[3].Replace("\"", "");
                            row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                            row["Active"] = rows[5].Replace("\"", "");
                            ip.Rows.Add(row);
                        }
                    }
                    else if (test == "WS")
                    {
                        if (rows[1].Replace("\"", "").StartsWith("ws"))
                        {
                            DataRow row = ip.NewRow();
                            row["Location"] = rows[0].Replace("\"", "");
                            row["Service"] = rows[1].Replace("\"", "");
                            row["Machine"] = rows[2].Replace("\"", "");
                            row["IP"] = rows[3].Replace("\"", "");
                            row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                            row["Active"] = rows[5].Replace("\"", "");
                            ip.Rows.Add(row);
                        }
                    }
                    else if (test == "GOLD")
                    {
                        if (rows[1].Replace("\"", "").StartsWith("gold"))
                        {
                            DataRow row = ip.NewRow();
                            row["Location"] = rows[0].Replace("\"", "");
                            row["Service"] = rows[1].Replace("\"", "");
                            row["Machine"] = rows[2].Replace("\"", "");
                            row["IP"] = rows[3].Replace("\"", "");
                            row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                            row["Active"] = rows[5].Replace("\"", "");
                            ip.Rows.Add(row);
                        }
                    }
                    else
                    {
                        DataRow row = ip.NewRow();
                        row["Location"] = rows[0].Replace("\"", "");
                        row["Service"] = rows[1].Replace("\"", "");
                        row["Machine"] = rows[2].Replace("\"", "");
                        row["IP"] = rows[3].Replace("\"", "");
                        row["Expected"] = rows[4].Replace("\"", "").Replace("\\", "");
                        row["Active"] = rows[5].Replace("\"", "");
                        ip.Rows.Add(row);
                    }
                }
            }
            //This stores the emails and their expected results into a list
            testEmails = new List<string>();
            using (StreamReader read = new StreamReader(File.OpenRead(emailfilePath)))
            {
                string email = "";
                while ((email = read.ReadLine()) != null)
                {
                    testEmails.Add(email);
                }
            }
            recieveEmails = new List<string>();
            using (StreamReader read = new StreamReader(File.OpenRead(recieveEmail)))
            {
                string email = "";
                while ((email = read.ReadLine()) != null)
                {
                    recieveEmails.Add(email);
                }
            }
        }
        public static void InitiallizeLogFileName()
        {
            using (StreamReader read = new StreamReader(lastFileCache))
            {
                string temp = "";
                if ((temp = read.ReadLine()) == "")
                {
                    lastFileused = Path.Combine(savepath, $"EmailMonitorLog-{DateTime.UtcNow.ToString("dd-MM-yyyy-HH-mm")}.txt");
                }
                else
                {
                    lastFileused = temp;
                    date = read.ReadLine();
                }
            }
        }
        //the code that runs the test contiuously with a given interval inbetween tests in minutes
        public static void StartTest()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            timedTest();
            watch.Stop();
        }
        //the code for each test
        public static void timedTest()
        {
            MailClient client = new MailClient();
            passlog = "";
            failedlog = "";
            failedCount = 0;
            foreach (DataRow row in ip.Rows)
            {
                string ips = row["IP"].ToString();
                string service = row["Service"].ToString();
                string location = row["Location"].ToString();
                string machine = row["Machine"].ToString();
                string active = row["Active"].ToString();
                string[] email = randomEmail().Split('\t');
                callEmail(email[0], location, email[1], ips, service, machine);
            }
            saveValues();
            foreach (string s in recieveEmails)
            {
                client.SendEmail($"EmailMonitorLog-{test}-{DateTime.UtcNow.ToString("dd-MM-yyyy-HH-mm")}", s, formatMessage(), $"GlobalEmailMonitor{test}@melissadata.com", test);
            }
            Console.WriteLine("end");
        }

        //This formats the email message to contain the logs of the Passes and Fails
        public static string formatMessage()
        {
            if (failedCount == 0)
            {
                return $"{DateTime.UtcNow.ToString()}</br>Testing Email System:<br/><br/>There are no fails in the service.<br/><br/>PASS:<br/>{passlog.Replace(";", "<br/>")}";
            }
            else
            {
                return $"{DateTime.UtcNow.ToString()}</br>Testing Email System:<br/>There is a total of {failedCount} fails in the service.<br/><br/>Fail:<br/><br/>{failedlog.Replace(";", "<br/>")}<br/><br/>PASS:<br/>{passlog.Replace(";", "<br/>")}";
            }
        }

        //Calls the email service to get the actual responses
        public static void callEmail(string inputEmail, string location, string expected, string ips, string service, string machinename)
        {
            inputEmail = inputEmail.Split('\t')[0];
            if (service == "globalemail.melissadata.net")
            {
                changeIP(ips, service);
                string httpstatus = "";
                Stopwatch timer = new Stopwatch();
                Responses.GlobalResponse root = null;
                string time = ""; timer.Start();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"http://{service}/V3/WEB/GlobalEmail/doGlobalEmail?t=1&id={customerLicense}&opt=VerifyMailbox:Premium,cache:off&email={inputEmail}&format=json");
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    timer.Stop();
                    time = timer.Elapsed.TotalMilliseconds.ToString();
                    httpstatus = response.StatusCode.ToString();

                    using (StreamReader read = new StreamReader(response.GetResponseStream()))
                    {
                        root = JsonConvert.DeserializeObject<Responses.GlobalResponse>(read.ReadToEnd());
                    }
                    if (root.TotalRecords == "1")
                    {
                        saveLog(inputEmail.Replace(expected, ""), location, time, httpstatus, expected, root.Records[0].Results, ips, service, machinename, customerLicense);
                    }
                }
                catch (WebException)
                {
                    saveLog(inputEmail.Replace(expected, ""), location, @"N/A", httpstatus, expected, @"N/A", ips, service, machinename, customerLicense);
                }
                catch (Exception)
                {
                    saveLog(inputEmail.Replace(expected, "").Replace(expected, ""), location, time, httpstatus, expected, root.TransmissionResults, ips, service, machinename, customerLicense);
                }
            }
            else if (service == "proxyemailrouterrc.melissadata.com")
            {
                changeIP(ips, service);
                string httpstatus = "";
                Stopwatch timer = new Stopwatch();
                Responses.ProxyResponse root = null;
                string time = "";
                timer.Start();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"http://proxyemailrouterrc.melissadata.com/v3/web/MailboxMaster/DoLookup?address={inputEmail}&apikey={internalLicense}");
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    timer.Stop();
                    time = timer.Elapsed.TotalMilliseconds.ToString();
                    httpstatus = response.StatusCode.ToString();
                    using (StreamReader read = new StreamReader(response.GetResponseStream()))
                    {
                        root = JsonConvert.DeserializeObject<Responses.ProxyResponse>(read.ReadToEnd());
                    }
                    saveLog(inputEmail.Replace(expected, ""), location, time, httpstatus, expected, root.status, ips, service, machinename, internalLicense);
                }
                catch (WebException)
                {
                    saveLog(inputEmail.Replace(expected, ""), location, @"N/A", httpstatus, expected, @"N/A", ips, service, machinename, internalLicense);
                }
                catch (Exception)
                {
                    saveLog(inputEmail.Replace(expected, ""), location, time, httpstatus, expected, root.status, ips, service, machinename, internalLicense);
                }
            }
            else if (service == "ebpemailrouter.melissadata.com")
            {
                changeIP(ips, service);
                string httpstatus = "";
                Stopwatch timer = new Stopwatch();
                string time = "";
                Responses.ProxyResponse root = null;
                timer.Start();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"http://ebprouter.melissadata.com/v3/web/MailboxMaster/DoLookup?address={inputEmail}&apikey={internalLicense}");
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    timer.Stop();
                    time = timer.Elapsed.TotalMilliseconds.ToString();
                    httpstatus = response.StatusCode.ToString();
                    using (StreamReader read = new StreamReader(response.GetResponseStream()))
                    {
                        root = JsonConvert.DeserializeObject<Responses.ProxyResponse>(read.ReadToEnd());
                    }
                    saveLog(inputEmail.Replace(expected, ""), location, time, httpstatus, expected, root.status, ips, service, machinename, internalLicense);

                }
                catch (WebException)
                {
                    saveLog(inputEmail.Replace(expected, ""), location, @"N/A", httpstatus, expected, @"N/A", ips, service, machinename, internalLicense);
                }
                catch (Exception)
                {
                    saveLog(inputEmail.Replace(expected, ""), location, time, httpstatus, expected, root.status, ips, service, machinename, internalLicense);
                }
            }
            else
            {
                string httpstatus = "";
                Stopwatch timer = new Stopwatch();
                string time = "";
                Responses.ProxyResponse root = null;
                timer.Start();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"http://{service}/EmailCheckWS/v3/WEB/MailboxChecker/DoLookup?APIKEY={internalLicense}&ADDRESS={inputEmail}");
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    timer.Stop();
                    time = timer.Elapsed.TotalMilliseconds.ToString();
                    httpstatus = response.StatusCode.ToString();
                    using (StreamReader read = new StreamReader(response.GetResponseStream()))
                    {
                        root = JsonConvert.DeserializeObject<Responses.ProxyResponse>(read.ReadToEnd());
                    }
                    saveLog(inputEmail.Replace(expected, ""), location, time, httpstatus, expected, root.status, ips, service, machinename, internalLicense);
                }
                catch (WebException)
                {
                    saveLog(inputEmail.Replace(expected, ""), location, @"N/A", httpstatus, expected, @"N/A", ips, service, machinename, internalLicense);
                }
                catch (Exception)
                {
                    saveLog(inputEmail.Replace(expected, ""), location, time, httpstatus, expected, root.status, ips, service, machinename, internalLicense);
                }
            }
        }
        // Saves the values into their respective logs depending on the actual response vs the expected response
        public static void saveLog(string inputEmail, string location, string timeElapsed, string httpstatus, string expected, string actual, string ip, string service, string machinename, string customerID)
        {
            actual = actual.Replace(" ", "").Replace("\"", "").Replace(",", "");
            expected = expected.Replace("\"", "").Replace("\\", "").Replace(" ", "").Replace(",", "");
            if (expected == actual)
            {
                index = 0;
                passlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual}|{expected}|{string.Format(@"{0:0.00}", timeElapsed)}|{httpstatus}|PASS;";
            }
            else if (actual == "ES03" || actual == "unknown")
            {
                index++;
                string newEmail = "";
                do
                {
                    newEmail = randomEmail();
                } while (newEmail == actual);
                if (index < 5)
                {
                    changeIP(ip, service);
                    callEmail(randomEmail(), location, expected, ip, service, machinename);
                }
                else
                {
                    failedlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual.Replace(",", "").Replace("\"", "")}||{timeElapsed}|{httpstatus}|FAIL;";
                }
            }
            else if (actual == "accept_all" && expected.Contains("ES01"))
            {
                index = 0;
                passlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual}|accept_all|{string.Format(@"{0:0.00}", timeElapsed)}|{httpstatus}|PASS;";
            }
            else if (actual == "valid" && expected.Contains("ES01"))
            {
                index = 0;
                passlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual}|valid|{string.Format(@"{0:0.00}", timeElapsed)}|{httpstatus}|PASS;";
            }
            else if (actual == "invalid" && expected.Contains("ES02"))
            {
                index = 0;
                passlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual}|invalid|{string.Format(@"{0:0.00}", timeElapsed)}|{httpstatus}|PASS;";
            }
            else
            {
                index = 0;
                failedCount++;
                if (actual == "valid" || actual == "accept_all")
                {
                    if (expected.Contains("ES01"))
                    {
                        failedlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual.Replace(",", "").Replace("\"", "")}|accept_all|{timeElapsed}|{httpstatus}|FAIL;";
                    }
                    else if (expected.Contains("ES02"))
                    {
                        failedlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual.Replace(",", "").Replace("\"", "")}|invalid|{timeElapsed}|{httpstatus}|FAIL;";
                    }
                    else if (expected.Contains("ES01"))
                    {
                        failedlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual.Replace(",", "").Replace("\"", "")}|valid|{timeElapsed}|{httpstatus}|FAIL;";
                    }
                }
                else if (actual == "invalid" && expected.Contains("ES01"))
                {
                    failedlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual.Replace(",", "").Replace("\"", "")}|valid|{timeElapsed}|{httpstatus}|FAIL;";
                }
                else
                {
                    failedlog += $"{Environment.MachineName}|{machinename}|{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{service}|{ip}|{location}|{inputEmail}|{actual.Replace(",", "").Replace("\"", "")}|{expected.Replace(",", "").Replace("\"", "")}|{timeElapsed}|{httpstatus}|FAIL;";
                }
            }
        }
        // selects a random email from the list
        public static string randomEmail()
        {
            return testEmails[(new Random()).Next(testEmails.Count)];
        }
        //alters the host file so that the webrequest will hit every proxy
        public static void changeIP(string ip, string service)
        {
            using (StreamWriter w = new StreamWriter((Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers/etc/hosts")), false))
            {
                w.WriteLine($"{ip} {service}");
            }
        }
        //Saves the logs into a pipe delimitted file
        public static void saveValues()
        {
            try
            {
                if (date != DateTime.UtcNow.ToString("dd-MM-yyyy"))
                {
                    date = DateTime.UtcNow.ToString("dd-MM-yyyy");
                    lastFileused = Path.Combine(savepath, $"EmailMonitorLog-{date}-{DateTime.UtcNow.ToString("HH-mm")}.txt");
                    using (StreamWriter write = new StreamWriter(lastFileused, false))
                    {
                        write.WriteLine("MachineName|TestMachineName|Date|Service|IP|Location|InputEmail|ActualResult|ExpectedResult|Time|HttpStatus|Result");
                    }
                    using (StreamWriter write = new StreamWriter(lastFileCache, false))
                    {
                        write.WriteLine(lastFileused);
                        write.WriteLine(date);
                    }
                }
                using (StreamWriter write = new StreamWriter(lastFileused, true))
                {
                    write.Write(failedlog.Replace(";", Environment.NewLine));
                    write.Write(passlog.Replace(";", Environment.NewLine));
                }
            }
            catch (Exception)
            {
                date = DateTime.UtcNow.ToString("dd-MM-yyyy");
                lastFileused = Path.Combine(savepath, $"EmailMonitorLog-{date}-{DateTime.UtcNow.ToString("HH-mm")}.txt");
                using (StreamWriter write = new StreamWriter(lastFileused, false))
                {
                    write.WriteLine("MachineName|TestMachineName|Date|Service|IP|Location|InputEmail|ActualResult|ExpectedResult|Time|HttpStatus|Result");
                    write.Write(failedlog.Replace(";", Environment.NewLine));
                    write.Write(passlog.Replace(";", Environment.NewLine));
                }
                using (StreamWriter write = new StreamWriter(lastFileCache, false))
                {
                    write.WriteLine(lastFileused);
                    write.WriteLine(date);
                }
            }
        }
    }
}
