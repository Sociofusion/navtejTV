using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("EventRegistration")]
    public class PEventRegistration
    {
        [Key]
        public Int32 ID { get; set; }

        public Int32 EventID { get; set; }

        public Int32 CustomerID { get; set; }

        public Int32 EventPaymentID { get; set; }

        public DateTime CreatedOn { get; set; }

        public Int32 CreatedBy { get; set; }

        public Int32 ISActive { get; set; }

    }
}
