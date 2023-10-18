using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navtezcms.BO
{
    public class FrontHeaderMenu
    {
        public string MenuTitle { get; set; }
        public int ParentTypeID { get; set; }
        public string Slug { get; set; }
        public int MenuOrder { get; set; }
        public long ParentMenuID { get; set; }
        public List<FrontHeaderMenu> childs { get; set; } = new List<FrontHeaderMenu>();
        public int GotoStatePage { get; set; } = 0;
        public long ID { get; set; }

    }
}
