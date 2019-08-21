using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Yawei.SupportCore.SupportApi.EntityHelper;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_Modules")]
    public class Model
    {
        [Key]
        public string Guid { set; get; }
        public string TopGuid { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string FullName { set; get; }
        public string Desp { set; get; }
        public string Sign { set; get; }
        public string Identity { set; get; }


        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddModel(Model model)
        {
            return ModelHelper.AddModel(model);
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int DeleteModel(Model model)
        {
            return ModelHelper.DeleteModel(model);
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateModel(Model model)
        {
            return ModelHelper.UpdateModel(model);
        }
    }
}
