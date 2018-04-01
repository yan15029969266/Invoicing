using Common;
using DataModel;
using InvoicingSystemWeb.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicingSystemWeb.Controllers
{
    
    public class ComponentController : BaseController
    {
        // GET: Component
        #region menu
        [Authentication]
        [UserPermission]
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult MenuList()
        {
            List<Sys_MenuModel> resultList = new List<Sys_MenuModel>();
            string url = string.Format("{0}/Component/GetMenuList", ConfigurationManager.AppSettings["APIAddress"]);
            List<Sys_MenuModel> list = HttpClientHelpClass.GetResponse<List<Sys_MenuModel>>(url, ConfigurationManager.AppSettings["APIToken"]);
            foreach (Sys_MenuModel menu in list)
            {
                menu.menuName = "<i class='fa fa-fw fa-minus treeMinus' data-up='0' data-menuid='" + menu.menuID.ToString() + "'></i>" + menu.menuName;
                resultList.Add(menu);
                foreach(Sys_MenuModel subMenu in menu.subMenuList)
                {
                    subMenu.menuName = "<i data-up='1' data-parentid='" + subMenu.parentID.ToString() + "'></i>" + subMenu.menuName;
                    resultList.Add(subMenu);
                }
            }
            int pageIndex = GetPageIndex();
            int pageSize = 20;
            return Json(new
            {
                iDisplayStart = pageSize,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = resultList
            }
            , JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddMenu()
        {
            Sys_MenuModel model = new Sys_MenuModel { menuID = Guid.NewGuid() };
            ViewBag.menu = GetMenuSelectList();
            return PartialView("MenuForm", model);
        }
        [HttpPost]
        public ActionResult AddMenu(Sys_MenuModel model)
        {
            InsertBaseData(model);
            if (model.parentID == Guid.Empty)
            {
                model.menuLevel = 1;
            }
            else
            {
                model.menuLevel = 2;
            }
            try
            {
                string url = string.Format("{0}/Component/InsertMenu", ConfigurationManager.AppSettings["APIAddress"]);
                string statusCode = string.Empty;
                bool isSuccess =Convert.ToBoolean(HttpClientHelpClass.PostResponse<Sys_MenuModel>(url, model, ConfigurationManager.AppSettings["APIToken"], out statusCode));
                if(isSuccess)
                {
                    return Json(new OperationResult(OperationResultType.Success, "添加成功！"));
                }
                else
                {
                    return Json(new OperationResult(OperationResultType.Warning, "添加失败！"));
                }
            }
            catch(Exception e)
            {
                return Json(new OperationResult(OperationResultType.Warning, "添加失败！", e.Message));
            }
            //AuthenticationBll bll = new AuthenticationBll();
            //Mapper.CreateMap<MenuInfoModel, Sys_MenuInfo>(); // 配置
            //Sys_MenuInfo menu = Mapper.Map<MenuInfoModel, Sys_MenuInfo>(model); // 使用AutoMapper自动映射
            //int row = bll.InsertMenu(menu);
            //if (row > 0)
            //{
            //    return Json(new OperationResult(OperationResultType.Success, "添加成功！"));
            //}
            //else
            //{
            //    return Json(new OperationResult(OperationResultType.Warning, "添加失败！"));
            //}
        }
        #region 加载按钮一级菜单
        private List<SelectListItem> GetMenuSelectList()
        {
            List<SelectListItem> menuList = new List<SelectListItem>();
            menuList.Add(new SelectListItem { Text = "无", Value = "" });
            string url = string.Format("{0}/Component/GetMenuList", ConfigurationManager.AppSettings["APIAddress"]);
            List<Sys_MenuModel> list = HttpClientHelpClass.GetResponse<List<Sys_MenuModel>>(url, ConfigurationManager.AppSettings["APIToken"]);
            IEnumerable<Sys_MenuModel > Enum = list.Where(t => t.menuLevel == 1);
            foreach (var model in Enum)
            {
                menuList.Add(new SelectListItem { Text = model.menuName, Value = model.menuID.ToString() });
            }
            return menuList;
        }
        #endregion
        #endregion
        #region button
        
        #endregion
    }
}