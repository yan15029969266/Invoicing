using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using CoreLogic.Interface;
using CoreLogic.Implementation;
using System.Collections.Generic;
using DBDataModel;
using System.Linq;

namespace InvoicingSystemAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Login()
        {

            ////创建容器
            //UnityContainer container = new UnityContainer();
            ////注册依赖对象
            //container.RegisterType<IAccountLogic, AccountLogic>();
            ////返回调用者
            //IAccountLogic a1 = container.Resolve<AccountLogic>();

            //a1.Login();

            //创建容器
            UnityContainer container2 = new UnityContainer();
            UnityConfigurationSection config = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            //加载到容器
            config.Configure(container2, "MyContainer");

            //返回调用者
            IAccountLogic IUser = container2.Resolve<IAccountLogic>();
            //执行
            //IUser.Login();
        }
        [TestMethod]
        public void TestSort()
        {
            int a = 1;
            string b = a.ToString("00000");
            int c = 111;
            string d = c.ToString("00000");
            List<Employe> list = new List<Employe>
            {
                new Employe {employeNo="E008" },
                new Employe {employeNo="E001" },
                new Employe {employeNo="E004" }
            };
            list = list.OrderBy(t => t.employeNo).ToList();
        }
    }
}
