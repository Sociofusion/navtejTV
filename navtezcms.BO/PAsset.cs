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
    [Table("Asset")]
    public class PAsset
    {
        [ExplicitKey]
        public Int64 ID { get; set; }

        public String UniqueName { get; set; }
        public String ActualName { get; set; }
        public String PreviewImageForVideo { get; set; }
        public int ParentTypeID { get; set; }
        public long ParentID { get; set; }

        public string LinkToUrl { get; set; }
        public int ImageOverlayTextContentId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long CreatedBy { get; set; }
        public int ISActive { get; set; }
        public int ISGallery { get; set; }
        public string ContentType { get; set; }

        [Write(false)]
        public string AssetLiveUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["ChangeRoomGoogleCloudCdnDisplayPath"]) + "/" + ParentType + "/" + UniqueName;
            }
        }

        [Write(false)]
        public string ParentType { get; set; }
    }
}
