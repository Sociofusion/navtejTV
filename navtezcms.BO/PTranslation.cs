using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Translation")]
    public class PTranslation
    {
        [ExplicitKey]
        public long TextContentID { get; set; }
        
        [ExplicitKey]
        public int LanguageID { get; set; }

        public String Translation { get; set; }

        public long RecordID { get; set; }

        [Write(false)]
        public string Language { get; set; }

        public int ParentTypeID { get; set; }
    }
}
