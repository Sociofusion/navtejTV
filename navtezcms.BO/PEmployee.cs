using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Employee")]
    public class PEmployee
    {
        [Key]
        public Int32 ID { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String Mobile { get; set; }

        public String Password { get; set; }

        public Int32 RoleID { get; set; }

        public Int32 ISActive { get; set; }

        public DateTime LastLoginTime { get; set; }

        public String Photo { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        [Write(false)]
        public String RoleName { get; set; }

        public enum ERole
        {
            admin = 1,
            customer = 2,
            moderator = 3,
            reporter = 5,
            superadmin = 6

        }

    }
}
