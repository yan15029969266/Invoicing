using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDataModel
{
    public class AuthModel
    {
        public Guid roleId { get; set; }
        public List<PMenuAuth> menuList { get; set; }
    }

    public class PMenuAuth
    {
        public Guid menuID { get; set; }
        public string menuName { get; set; }
        public bool isSelected { get; set; }
        public List<CMenuAuth> cmenuList { get; set; }
    }
    public class CMenuAuth
    {
        public Guid menuID { get; set; }
        public string menuName { get; set; }
        public bool isSelected { get; set; }
        public List<ButtonAuth> buttonList { get; set; }
    }
    public class ButtonAuth
    {
        public Guid menu_btnID { get; set; }
        public string btnName { get; set; }
        public bool isSelected { get; set; }
    }
}
