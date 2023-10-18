using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navtezcms.api
{
    public class response
    {
        public string payload { get; set; }
        public int TotalRecords { get; set; } = 0;
        public int LastCounter { get; set; } = 0;
        public bool success { get; set; } = true;
        public List<string> messages { get; set; }
        public response()
        {
            success = true;
            messages = new List<string>();
        }
    }
}
