using DBDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLogic.Interface
{
    public interface IAccountLogic
    {
        Employe Login(string account, string pwd);
    }
}
