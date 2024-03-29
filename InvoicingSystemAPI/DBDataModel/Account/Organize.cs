﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class Organize
    {
        [Key]
        public Guid organizeID { get; set; }
        public string organizeName { get; set; }
        public string organizeCode { get; set; }
        public bool isIndependent { get; set; }
        public int depth { get; set; }
        public Guid parentID { get; set; }
        public string info { get; set; }
        public bool enable { get; set; }
        public DateTime ctime { get; set; }
        public Guid cid { get; set; }
        public DateTime uptime { get; set; }
        public Guid upid { get; set; }
    }
}
