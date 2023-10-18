using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
namespace navtezcms.BO
{
    [Table("ContactUs")]
    public class PContactUs
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
