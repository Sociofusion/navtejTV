using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Donation")]
    public class PDonation
    {
        [Key]
        public Int32 ID { get; set; }

        public Int32 CustomerID { get; set; }

        public Int32 DonationEventID { get; set; }

        public String FirstName { get; set; }

        public String MiddleName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String MobileNo { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String Address { get; set; }

        public Decimal DonationAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public Int32 ISSuccess { get; set; }

        public String ReceiptFileName { get; set; }

        public String Notes { get; set; }

    }
}
