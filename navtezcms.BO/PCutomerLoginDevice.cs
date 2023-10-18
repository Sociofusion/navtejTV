using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("CutomerLoginDevice")]
    public class PCutomerLoginDevice
    {
        [Key]
        public Int32 ID { get; set; }

        public Int32 CustomerID { get; set; }

        public DateTime LoginAt { get; set; }

        public String DeviceName { get; set; }

        public String DeviceUID { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
