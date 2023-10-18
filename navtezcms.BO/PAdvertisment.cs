using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
namespace navtezcms.BO
{
    [Table("Advertisment")]
    public class PAdvertisment
    {
        [ExplicitKey]
        public long ID { get; set; }
        public long PlacementAreaID { get; set; }
        public string AdType { get; set; }
        public string AdSize { get; set; }
        public string AdCode { get; set; }
        public string AdLink { get; set; }
        public int ISActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);
        public int CreatedBy { get; set; }

        [Write(false)]
        public string PlacementAreaName { get; set; }
        [Write(false)]
        public PAsset pAsset { get; set; }

        [Write(false)]
        public string AssetFullUrl { get; set; }

        public enum EAdType
        {
            FromAsset=1,
            FromCode
        }

        [Write(false)]
        public int ISNew { get; set; } = 0;
    }
}
