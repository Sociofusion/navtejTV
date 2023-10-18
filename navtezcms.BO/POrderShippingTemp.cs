using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("OrderShippingTemp")]
    public class POrderShippingTemp
    {
        [Key]
        public Int32 OrderShippingID { get; set; }

        public Int32 OrderID { get; set; }

        public String CustomerID { get; set; }

        public String ContactPerson { get; set; }

        public String Email { get; set; }

        public String Mobile { get; set; }

        public String Mobile1 { get; set; }

        public String Address { get; set; }

        public String Landmark { get; set; }

        public String Country { get; set; }

        public String State { get; set; }

        public String City { get; set; }

        public String ZIP { get; set; }

        public String Extra1 { get; set; }

        public String Extra2 { get; set; }

        public String Extra3 { get; set; }

        public Boolean IsActive { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}
