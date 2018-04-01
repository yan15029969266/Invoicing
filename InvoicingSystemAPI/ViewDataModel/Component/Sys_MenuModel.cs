using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDataModel.Component
{
    public class Sys_MenuModel:BaseModel
    {
        public Guid menuID { get; set; }
        public string menuName { get; set; }
        public string menuUrl { get; set; }
        public string menuIcon { get; set; }
        public int menuLevel { get; set; }
        public Guid parentID { get; set; }
        public int sort { get; set; }
        public bool enable { get; set; }
        public List<Sys_MenuModel> subMenuList { get; set; }
    }
}
