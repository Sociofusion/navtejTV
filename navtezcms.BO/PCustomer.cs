using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Customer")]
    public class PCustomer
    {
        [Key]
        public Int32 ID { get; set; }

        public String FirstName { get; set; }

        public String MiddleName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String MobileNo { get; set; }

        public String Address { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public Int32 ISActive { get; set; }

        public DateTime LastLogin { get; set; }

        public Int32 ISCurrentlyLogin { get; set; }

        public Int32 MobileNoVerified { get; set; }

        public String GeneratedOTP { get; set; }

        public DateTime CreatedOn { get; set; }

        public Int32 CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Int32 ModifiedBy { get; set; }

        public string Password { get; set; }

    }
}
