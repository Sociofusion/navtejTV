using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Language")]
    public class PLanguage
    {
        [Key]
        public Int32 ID { get; set; }

        public String Name { get; set; }

        public Int32 ISDefault { get; set; }

        public Int32 ISActive { get; set; }

    }
}
