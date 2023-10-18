using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Banner")]
    public class PBanner
    {
        [Key]
        public Int32 ID { get; set; }

        public String BannerImageName { get; set; }

        public Int32 ParentTypeID { get; set; }

        public String LinkToUrl { get; set; }

        public String BannerText { get; set; }

        public Int32 ParentID { get; set; }

        public DateTime CreatedOn { get; set; }

        public Int32 CreatedBy { get; set; }

        public Int32 ISActive { get; set; }

    }
}
