using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using System.Configuration;

namespace navtezcms.BO
{
    [Table("Setting")]
    public class PSetting
    {
        [Key]
        public Int64 ID { get; set; }

        public String FacebookLink { get; set; }

        public String TwitterLink { get; set; }

        public String InstagramLink { get; set; }

        public String YoutubeLink { get; set; }

        public String YoutubeVideoURL { get; set; }

        public String Logo { get; set; }

        public String FooterLogo { get; set; }




        public String MetaTags { get; set; }

        public String GoogleAnalytics { get; set; }

        public String Address { get; set; }

        public String MailID { get; set; }

        public String Mobile1 { get; set; }

        public String Mobile2 { get; set; }

        public String Copyright { get; set; }
        



        [Write(false)]
        public string LogoLiveUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["ChangeRoomGoogleCloudCdnDisplayPath"]) + "/setting/" + Logo;
            }
        }

        [Write(false)]
        public string FooterLogoLiveUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["ChangeRoomGoogleCloudCdnDisplayPath"]) + "/setting/" + FooterLogo;
            }
        }

        public Int32 ISActive { get; set; }
    }
}
