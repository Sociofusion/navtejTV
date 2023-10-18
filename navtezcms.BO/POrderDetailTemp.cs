using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
namespace navtezcms.BO
{
    [Table("OrderDetailTemp")]
    public class POrderDetailTemp
    {
        [Key]
        public Int32 OrderDetailID { get; set; }

        public Int32 OrderID { get; set; }

        public Int32 ProductID { get; set; }

        public String ProductName { get; set; }

        public Int32 ProductQuantity { get; set; }

        public Decimal ProductPrice { get; set; }

        public String Discount { get; set; }

        public Boolean IsActive { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime LastUpdate { get; set; }

        public String SubInvoiceNo { get; set; }

        public Decimal TotalAmount { get; set; }

        public String Status { get; set; }

        public DateTime DispatchDate { get; set; }

        public Boolean IsCancel { get; set; }

        public String Remark { get; set; }

        public String CancelReason { get; set; }

        public String Comments { get; set; }

        public Boolean IsShipping { get; set; }

        public Boolean IsProcessing { get; set; }

        public Boolean IsDelivered { get; set; }

        public DateTime DeliveryDate { get; set; }

        public String ProductColor { get; set; }

        public String ProductSize { get; set; }

        public String OfferCode { get; set; }

        public Decimal OfferDiscount { get; set; }

        public Decimal Shipping { get; set; }

        public Boolean PayReceived { get; set; }

        public Int32 Off { get; set; }

        public String OldPrice { get; set; }

    }
}
