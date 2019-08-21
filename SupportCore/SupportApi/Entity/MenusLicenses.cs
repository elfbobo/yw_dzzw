using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Yawei.SupportCore.SupportApi.EntityHelper;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_MenuLicenses")]
    public class MenusLicenses
    {
        [Key, Column(Order = 0)]
        public string MenuGuid { set; get; }
        [Key, Column(Order = 1)]
        public string RoleGuid { set; get; }
        public int Type { get; set; }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="ml"></param>
        /// <returns></returns>
        public static int AddMenuLies(MenusLicenses ml)
        {
            return ModelHelper.AddMenuLicenses(ml);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="ml"></param>
        /// <returns></returns>
        public static int DeleteMenuLies(MenusLicenses ml)
        {
            return ModelHelper.DeleteMenuLicenses(ml);
        }
    }
}
