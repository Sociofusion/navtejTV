using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("CustomPage")]
    public class PCustomPage
    {
        [ExplicitKey]
        public Int64 ID { get; set; }

        public String PageName { get; set; }

        public Int64 TitleContentID { get; set; }

        public Int64 DescriptionContentID { get; set; }

        public String Slug { get; set; }

        public String MetaTags { get; set; }

        public Int32 CreateById { get; set; }

        public DateTime CreatedOn { get; set; }

        public Int32 ISActive { get; set; }

        public Int32 ISFooter { get; set; }

        [Write(false)]
        public List<PTranslation> TitleData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultTitleToDisplay { get; set; }

        [Write(false)]
        public List<PTranslation> DescriptionData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultDescriptionToDisplay { get; set; }
    
        [Write(false)]
        public string Language { get; set; }

        [Write(false)]
        public int DefaultLanguageID { get; set; }

    }
}
