using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Menu")]
    public class PMenu
    {
        [ExplicitKey]
        public long ID { get; set; }

        public long ParentMenuID { get; set; }

        public String LinkUrl { get; set; }

        public Int32 ISActive { get; set; }

        public Int64 MenuTextContentID { get; set; }

        public int MenuOrder { get; set; }

        public bool ISFooter { get; set; }

        public int ParentTypeID { get; set; }

        public string FooterSlug { get; set; }

        [Write(false)]
        public List<PTranslation> TitleData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultTitleToDisplay { get; set; }

        [Write(false)]
        public string Language { get; set; }

        [Write(false)]
        public int DefaultLanguageID { get; set; }

        [Write(false)]
        public string ParentType { get; set; }
        
    }
}
