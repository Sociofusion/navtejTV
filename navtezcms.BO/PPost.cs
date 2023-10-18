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
    [Table("Post")]
    public class PPost
    {
        [ExplicitKey]
        public Int64 ID { get; set; }

        public Int64 TitleContentID { get; set; }

        public string Slug { get; set; }

        public Int32 PostTypeID { get; set; }

        public Int64 MetaTagContentID { get; set; }

        public Int32 ISShowRightColumn { get; set; }

        public Int32 ISFeature { get; set; }

        public Int32 ISSlider { get; set; }

        public Int32 ISSliderLeft { get; set; }

        public Int32 ISSliderRight { get; set; }

        public Int32 ISTrending { get; set; }

        public Int32 ISVideoGallery { get; set; }

        public String Tags { get; set; }

        public Int64 DescriptionContentID { get; set; }

        public String ImageBig { get; set; }

        public int ImageBigAssetID { get; set; }

        public String RssImage { get; set; }

        public String ImageSmall { get; set; }

        public String Video { get; set; }

        public String EmbedVideo { get; set; }

        public String Audio { get; set; }

        public Int32 CategoryID { get; set; }

        public Int32 SubCategoryID { get; set; }

        public Int32 ISSchedulePost { get; set; }

        public DateTime? SchedulePostDate { get; set; }
        public DateTime? SchedulePostEndDate { get; set; }

        public Int32 CreatedBy { get; set; }

        public Int32 ModifiedBy { get; set; }

        public Int32 StatusID { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Int32 ISActive { get; set; }

        public String EmbedSocial { get; set; }

        public Int32 ISFacebookEmbed { get; set; }
        public Int32 ISInstagramEmbed { get; set; }
        public Int32 ISTwitterEmbed { get; set; }
        public Int32 ISYoutubeEmbed { get; set; }


        [Write(false)]
        public List<PTranslation> TitleData { get; set; } = new List<PTranslation>();
        [Write(false)]
        public List<PTranslation> DescriptionData { get; set; } = new List<PTranslation>();
        [Write(false)]
        public List<PTranslation> MetaTagData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string FeatureImageUrl { get; set; }

        [Write(false)]
        public string DefaultTitleToDisplay { get; set; }

        [Write(false)]
        public string DefaultDescriptionToDisplay { get; set; }

        [Write(false)]
        public string DefaultMetaTagToDisplay { get; set; }

        [Write(false)]
        public string CategoryName { get; set; }

        [Write(false)]
        public string PostType { get; set; }

        [Write(false)]
        public string Author { get; set; }

        [Write(false)]
        public string Language { get; set; }

        [Write(false)]
        public string CreatedOnStr
        {
            get
            {
                if (CreatedOn.HasValue)
                {
                    return CreatedOn.Value.ToString("MMM dd, yyyy");
                }
                else
                { return ""; }
            }
        }


        [Write(false)]
        public string ScheduleDateStr
        {
            get
            {
                if (ISSchedulePost == 1 && SchedulePostDate.HasValue)
                {
                    return SchedulePostDate.Value.ToString("dd-MMM-yyyy HH:mm");
                }
                else
                { return ""; }
            }
        }

        [Write(false)]
        public string ScheduleDate_ddMMyyyy
        {
            get
            {
                if (ISSchedulePost == 1 && SchedulePostDate.HasValue)
                {
                    return SchedulePostDate.Value.ToString("dd/MM/yyyy HH:mm");
                }
                else
                { return ""; }
            }
        }
        [Write(false)]
        public string ScheduleEndDate_ddMMyyyy
        {
            get
            {
                if (ISSchedulePost == 1 && SchedulePostEndDate.HasValue)
                {
                    return SchedulePostEndDate.Value.ToString("dd/MM/yyyy HH:mm");
                }
                else
                { return ""; }
            }
        }

        [Write(false)]
        public List<PAsset> PostFiles { get; set; } = new List<PAsset>();

        [Write(false)]
        public PAsset pAsset { get; set; }

        [Write(false)]
        public string AssetFullUrl { get; set; }

        [Write(false)]
        public List<PAsset> pAssetGallery { get; set; } = new List<PAsset>();

        [Write(false)]
        public string AssetLiveUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["ChangeRoomGoogleCloudCdnDisplayPath"]) + "/Post/" + AssetUniqueName;
            }
        }

        [Write(false)]
        public string AssetUniqueName { get; set; }

        [Write(false)]
        public string AssetActualName { get; set; }

        [Write(false)]
        public String CategoryColor { get; set; }

    }
}
