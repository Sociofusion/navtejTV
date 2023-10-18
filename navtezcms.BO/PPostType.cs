using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("PostType")]
    public class PPostType
    {
        [Key]
        public Int32 ID { get; set; }

        public String PostType { get; set; }

        public int ISActive { get; set; }
    }
}
