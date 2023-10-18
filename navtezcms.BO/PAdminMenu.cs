using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("AdminMenu")]
    public class PAdminMenu
    {
        [Key]
        public long ID { get; set; }

        public String MenuName { get; set; }

        public Int32 ParentID { get; set; }

        public string MenuLink { get; set; }

        public Int32 MenuLevel { get; set; }

        public string MenuStr { get; set; }

        public DateTime AddDate { get; set; }

        public bool ISActive { get; set; }

        public int Position { get; set; }

        public string Icon { get; set; }

        [Write(false)]
        public bool ISChecked { get; set; }

    }
}
