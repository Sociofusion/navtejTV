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
    [Table("Product")]
    public class PProduct
    {
        [ExplicitKey]
        public Int64 ID { get; set; }

        public String Slug { get; set; }

        public int CategoryID { get; set; }

        public int CreatedBy { get; set; }

        public Int64 TitleContentID { get; set; }

        public Int64 DescriptionContentID { get; set; }

        public Int64 ReturnPolicyContentID { get; set; }

        public decimal Price { get; set; }

        public int Off { get; set; }

        public decimal EffectivePrice { get; set; }

        public String ImageBig { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        
        public decimal ShippingCharge { get; set; }

        public int ISActive { get; set; }

        public int ISCOD { get; set; }

        public int ISDigitalProduct { get; set; }

        public int ISDigitalProductFree { get; set; }

        public String VideoProductURL { get; set; }
        
        [Write(false)]
        public List<PTranslation> TitleData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultTitleToDisplay { get; set; }

        [Write(false)]
        public List<PTranslation> DescriptionData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultDescriptionToDisplay { get; set; }

        [Write(false)]
        public List<PTranslation> ReturnPolicyData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultReturnPolicyToDisplay { get; set; }

        [Write(false)]
        public string Language { get; set; }

        [Write(false)]
        public int DefaultLanguageID { get; set; }

        [Write(false)]
        public string CategoryName { get; set; }

        [Write(false)]
        public string Author { get; set; }

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
                return Convert.ToString(ConfigurationManager.AppSettings["ChangeRoomGoogleCloudCdnDisplayPath"]) + "/Shop/" + AssetUniqueName;
            }
        }

        [Write(false)]
        public string AssetUniqueName { get; set; }

        [Write(false)]
        public string AssetActualName { get; set; }

    }
}
