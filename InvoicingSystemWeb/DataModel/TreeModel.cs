using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class TreeModel
    {
        public List<TreeData> data { get; set; }
    }
    public class TreeData
    {
        public TreeData()
        {
            childrens = new List<TreeData>();
        }
        public Guid id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public Guid pid { get; set; }
        public List<TreeData> childrens { get; set; }
    }
}
