using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace navtezcms.BO
{
    public class PUser
    {
        public Int32 ID { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String Phone { get; set; }

        public Int32 RoleID { get; set; }

        public String Photo { get; set; }

        public String Password { get; set; }

        public String Token { get; set; }

        public Int16 ISVerified { get; set; }

        public Int16 StatusID { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}
