using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("GeneralSetting")]
    public class PGeneralSetting
    {
        [Key]
        public Int32 ID { get; set; }

        public String Logo { get; set; }

        public String FooterLogo { get; set; }

        public String Favicon { get; set; }

        public String Loader { get; set; }

        public Int32 TitleContentID { get; set; }

        public String ThemeColor { get; set; }

        public String FooterColor { get; set; }

        public String TawkTo { get; set; }

        public Int16 ISTawkTo { get; set; }

        public String Disqus { get; set; }

        public Int16 ISDisqus { get; set; }

        public Int32 CopyrightTextContentID { get; set; }

        public String CopyrightColor { get; set; }

        public Int32 FooterTextContentID { get; set; }

        public String Tags { get; set; }

        public String SmtpHost { get; set; }

        public String SmtpPort { get; set; }

        public String SmtpUser { get; set; }

        public String SmtpPass { get; set; }

        public String FromEmail { get; set; }

        public String FromName { get; set; }

        public String Timezone { get; set; }

        public Int16 ISSmtp { get; set; }

    }
}
