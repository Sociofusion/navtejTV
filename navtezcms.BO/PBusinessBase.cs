using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("BusinessBase")]
    public class PBusinessBase
    {
        [Key]
        public int ID { get; set; }
        public string CreatedOn { get; set; }
    }
}
