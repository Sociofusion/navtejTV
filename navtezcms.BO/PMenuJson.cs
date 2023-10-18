using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("MenuJson")]
    public class PMenuJson
    {
        [Key]
        public Int32 ID { get; set; }

        public String JsonString { get; set; }

        public Int32 LanguageID { get; set; }
        
        public DateTime LastUpdate { get; set; }

    }
}
