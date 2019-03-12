using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    class Responses
    {
        //The Json response class that gets returned in the globalemail.melissadata.net web request
        public class GlobalResponse
        {
            public string Version { get; set; }
            public string TransmissionReference { get; set; }
            public string TransmissionResults { get; set; }
            public string TotalRecords { get; set; }
            public Record[] Records { get; set; }
        }
        public class Record
        {
            public string RecordID { get; set; }
            public string Results { get; set; }
            public string EmailAddress { get; set; }
            public string MailboxName { get; set; }
            public string DomainName { get; set; }
            public string TopLevelDomain { get; set; }
            public string TopLevelDomainName { get; set; }
            public string DateChecked { get; set; }
        }
        //the response class for the proxy web requests
        public class ProxyResponse
        {
            public string address { get; set; }
            public string account { get; set; }
            public string domain { get; set; }
            public string status { get; set; }
            public object connected { get; set; }
            public bool disposable { get; set; }
            public bool role_address { get; set; }
            public float duration { get; set; }
            public string result_code { get; set; }
            public string webServer { get; set; }
            public string mxServer { get; set; }
            public int mailboxStatus { get; set; }
            public float elapsedDns { get; set; }
            public float elapsedConnect { get; set; }
            public float elapsedConversation { get; set; }
            public string version { get; set; }
            public string expireDate { get; set; }
            public int resultLevel { get; set; }
            public DateTime dateChecked { get; set; }
            public string proxyUrl { get; set; }
        }
    }
}
