using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class Sys_Button
    {
        [Key]
        public Guid btnId { get; set; }
        public string btnName { get; set; }
        public string func { get; set; }
        public int sort { get; set; }
        public bool enable { get; set; }
        public DateTime ctime { get; set; }
        public Guid cid { get; set; }
        public DateTime uptime { get; set; }
        public Guid upid { get; set; }
    }
}
