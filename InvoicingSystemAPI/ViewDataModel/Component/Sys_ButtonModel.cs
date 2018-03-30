using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDataModel.Component
{
    public class Sys_ButtonModel : BaseModel
    {
        public int btnId { get; set; }
        public string btnName { get; set; }
        public string func { get; set; }
        public int sort { get; set; }
        public bool enable { get; set; }
    }
}
