using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("APISession")]
    public class PAPISession
    {
        [Key]
        public long APISession_Id { get; set; }
        public string APISession_Token { get; set; }
        public string APISession_CustomerID { get; set; }
        public DateTime APISession_CreatedOn { get; set; }
        public DateTime APISession_LastUsed { get; set; }
        public string APISession_UserHostAddress { get; set; }
        public string APISession_DeviceName { get; set; }
    }
}
