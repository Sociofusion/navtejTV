using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
namespace navtezcms.BO
{
    [Table("AdvertismentPlacement")]
    public class PAdvertismentPlacement
    {
        [ExplicitKey]
        public long ID { get; set; }
        public string PlacementAreaName { get; set; }
        public int ISActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
