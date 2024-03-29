﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Sys_ButtonModel:BaseModel
    {
        public Guid btnId { get; set; }
        public string btnName { get; set; }
        public string func { get; set; }
        public int sort { get; set; }
        public bool enable { get; set; }
        public string status
        {
            get
            {
                return enable ? "<span class='label label-success'>激活</span>" : "<span class='label label-danger'>禁止</span>";
            }
        }
    }
}
