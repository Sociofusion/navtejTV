using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Order")]
    public class POrder
    {
        [Key]
        public Int32 ID { get; set; }

        public Int32 CustomerID { get; set; }

        public String TransactionID { get; set; }

        public String OrderStatus { get; set; }

        public Decimal ProductAmount { get; set; }

        public String Discount { get; set; }

        public Boolean IsActive { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime LastUpdate { get; set; }

        public Int32 InvoiceNo { get; set; }

        public Int32 TotalQTY { get; set; }

        public Decimal TotalAmount { get; set; }

        public Int32 PaymentStatus { get; set; }

        public String PaymentStatusTitle { get; set; }

        public String OfferCode { get; set; }

        public String OfferAmount { get; set; }

        public Decimal FreeShippingAmount { get; set; }

        
        [Write(false)]
        public List<POrderDetail> LineItem { get; set; }

    }
}
