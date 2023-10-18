using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("RolePermission")]
    public class PRolePermission
    {
        [Key]
        public Int32 ID { get; set; }

        public Int32 RoleID { get; set; }

        public Int32 PermissionID { get; set; }

    }
}
