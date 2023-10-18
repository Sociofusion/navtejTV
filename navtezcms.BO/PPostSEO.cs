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
    public class PPostSEO
    {
        [ExplicitKey]
        public Int64 ID { get; set; }

        public Int64 TitleContentID { get; set; }

        public string Slug { get; set; }

        public Int32 PostTypeID { get; set; }

        public Int64 MetaTagContentID { get; set; }
        
        public String Tags { get; set; }

        public Int64 DescriptionContentID { get; set; }

        public String ImageBig { get; set; }
        
        [Write(false)]
        public List<PTranslation> TitleData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public List<PTranslation> MetaTagData { get; set; } = new List<PTranslation>();
        
        [Write(false)]
        public string DefaultTitleToDisplay { get; set; }

        [Write(false)]
        public string DefaultMetaTagToDisplay { get; set; }
        
        [Write(false)]
        public string AssetLiveUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["ChangeRoomGoogleCloudCdnDisplayPath"]) + "/Post/" + ImageBig;
            }
        }
    }
}
