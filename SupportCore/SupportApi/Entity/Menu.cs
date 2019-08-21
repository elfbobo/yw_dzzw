using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Yawei.SupportCore.SupportApi.EntityHelper;
using System.Xml;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_Menus")]
    public class Menu
    {
        [Key]
        public string Guid { get; set; }
        public string TopGuid { get; set; }
        public string Name { get; set; }
        public string IconCls { get; set; }
        public string ImgUrl { get; set; }
        public string Href { get; set; }
        public string Target { get; set; }
        public string JSEvent { get; set; }
        public int SortNum { get; set; }
        public int Status { get; set; }
        public string Sign { get; set; }




        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public static System.Xml.XmlDocument GetMenuXmlByUser(string userGuid)
        {
            return ModelHelper.GetMenuXml(userGuid);
        }

        /// <summary>
        /// 指定节点的用户权限菜单
        /// </summary>
        /// <param name="menuGuid">菜单节点</param>
        /// <returns></returns>
        public  XmlDocument GetMenuXml(string userGuid)
        {
            return ModelHelper.GetMenuXml(userGuid, Guid);
        }

        /// <summary>
        /// 获取菜单json
        /// </summary>
        /// <param name="path">虚拟目录</param>
        /// <returns></returns>
        public static  string GetMenuJSONByUser(string path, string userGuid)
        {
            string json = ModelHelper.GetMenuJSON(userGuid, path);
            return json;
        }

        /// <summary>
        /// 获取菜单json
        /// </summary>
        /// <param name="path">虚拟目录</param>
        /// <returns></returns>
        public  string GetMenuJSON(string path, string userGuid)
        {
            string json = ModelHelper.GetMenuJSON(userGuid, Guid, path);
            return json;
        }



        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static int AddMenu(Menu menu)
        {
            return ModelHelper.AddMenu(menu);
        }


        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static int DeleteMenu(Menu menu)
        {
            return ModelHelper.DeleteMenu(menu);
        }


        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static int UpdateMenu(Menu menu)
        {
            return ModelHelper.UpdateMenu(menu);
        }
    }
}
