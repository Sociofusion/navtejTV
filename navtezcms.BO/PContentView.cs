using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("ContentView")]
    public class PContentView
    {
        [Key]
        public Int32 ID { get; set; }

        public Int32 ParentID { get; set; }

        public Int32 ParentTypeID { get; set; }

        public String IPAddress { get; set; }

        public DateTime ViewDate { get; set; }

    }
}
