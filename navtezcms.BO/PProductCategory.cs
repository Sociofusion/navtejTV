using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
namespace navtezcms.BO
{
    [Table("ProductCategory")]
    public class PProductCategory
    {
        [ExplicitKey]
        public Int64 ID { get; set; }

        public Int64 TitleContentID { get; set; }

        public Int32 ParentCategoryID { get; set; }

        public String Slug { get; set; }

        public String Color { get; set; }

        public Int32 CategoryOrder { get; set; }

        public Int32 CanShowAtHome { get; set; }

        public Int32 CanShowOnMenu { get; set; }

        public Int32 ISActive { get; set; }

        [Write(false)]
        public string ExistingCategoryOrder { get; set; } = "";

        [Write(false)]
        public List<PTranslation> TitleData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultTitleToDisplay { get; set; }

        [Write(false)]
        public string Language { get; set; }

        [Write(false)]
        public int DefaultLanguageID { get; set; }
        
        [Write(false)]
        public bool ISChecked { get; set; }
    }
}
