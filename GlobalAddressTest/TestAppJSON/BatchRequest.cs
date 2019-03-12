using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppJSON
{
    class BatchRequest
    {
        public BatchRequest()
        {
            this.CustomerID = "DCkQd6ueSb72K87oKi-XlS**";
            this.Options = "";
            this.Records = new Record[0];
            this.TransmissionReference = "";
        }
        public void addRecord(Record record)
        {
            List<Record> rec = Records.ToList<Record>();
            rec.Add(record);
            Records = rec.ToArray();
        }
        public string TransmissionReference { get; set; }
        public string CustomerID { get; set; }
        public string Options { get; set; }
        public Record[] Records { get; set; }
        
    }
}
