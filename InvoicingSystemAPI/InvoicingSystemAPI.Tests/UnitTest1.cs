using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using CoreLogic.Interface;
using CoreLogic.Implementation;

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
    }
}
