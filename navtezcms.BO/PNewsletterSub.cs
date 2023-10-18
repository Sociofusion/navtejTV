using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("NewsletterSub")]
    public class PNewsletterSub
    {
        [Key]
        public long ID { get; set; }
        public string PersonEmail { get; set; }
    }
}
