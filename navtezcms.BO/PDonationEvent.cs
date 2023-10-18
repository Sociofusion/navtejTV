using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("DonationEvent")]
    public class PDonationEvent
    {
        [Key]
        public Int32 ID { get; set; }

        public Int64 TitleContentId { get; set; }

        public Int64 DescriptionContentID { get; set; }

        public String MetaTags { get; set; }

        public Decimal TargetAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public Int32 CreatedBy { get; set; }

        public Int32 ISActive { get; set; }

        [Write(false)]
        public List<PTranslation> TitleData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultTitleToDisplay { get; set; }

        [Write(false)]
        public List<PTranslation> DescriptionData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultDescriptionToDisplay { get; set; }

        [Write(false)]
        public int DefaultLanguageID { get; set; }

        [Write(false)]
        public string Language { get; set; }
    }
}
