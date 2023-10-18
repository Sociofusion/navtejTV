using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Status")]
    public class PStatus
    {
        [Key]
        public Int32 ID { get; set; }

        public String Status { get; set; }

    }
}
