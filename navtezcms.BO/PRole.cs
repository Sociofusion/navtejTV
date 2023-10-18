using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Role")]
    public class PRole
    {
        [Key]
        public Int32 ID { get; set; }

        public String RoleName { get; set; }

        public string MenuIds { get; set; }

        public string CategoryIds { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool ISShowAll { get; set; }

    }
}
