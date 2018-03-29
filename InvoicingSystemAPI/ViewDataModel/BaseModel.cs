using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDataModel
{
    public class BaseModel
    {
        public DateTime ctime { get; set; }
        public Guid cid { get; set; }
        public DateTime uptime { get; set; }
        public Guid upid { get; set; }
    }
}
