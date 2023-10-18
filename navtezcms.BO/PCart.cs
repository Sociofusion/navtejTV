using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Cart")]
    public class PCart
    {
        [Key]
        public Int32 ID { get; set; }

        public Int32 CustomerID { get; set; }

        public Int32 ProductID { get; set; }

        public Decimal Price { get; set; }

        public Int32 Quantity { get; set; }

        public String ProductName { get; set; }

        public String Image1 { get; set; }

        public String Color { get; set; }

        public String Size { get; set; }

        public String offercode { get; set; }

        public String offerDiscount { get; set; }

        public String Discount { get; set; }

        public String AfterDiscount { get; set; }

        public String shipping { get; set; }

        public String ReturnPolicy { get; set; }

        public String EstimatedDays { get; set; }

        public String CategoryName { get; set; }

        public String SubCategoryName { get; set; }

        public Boolean IsCOD { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
