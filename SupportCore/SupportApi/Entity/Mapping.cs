using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Yawei.SupportCore.SupportApi.EntityHelper;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_Mapping")]
    public class Mapping
    {
        [Key, Column(Order = 0)]
        public string Guid { set; get; }
        public string DirectoryGuid { set; get; }
        public string Name { set; get; }
        public string Substance { set; get; }
        public int Mark { set; get; }





        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int AddMapping(Mapping mapping)
        {
            return ModelHelper.AddMapping(mapping);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int AddMapping(List<Mapping> mapping)
        {
            return ModelHelper.AddMapping(mapping);
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int DeleteMapping(Mapping mapping)
        {
            return ModelHelper.DeleteMapping(mapping);
        }

        /// <summary>
        /// 更新字典
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int UpdateMapping(Mapping mapping)
        {
            return ModelHelper.UpdateMapping(mapping);
        }

        /// <summary>
        /// 获取一组字典集合
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static List<Mapping> GetMappingByGuid(string guid)
        {
            return ModelHelper.GetMappingByGuid(guid);
        }

        /// <summary>
        /// 根据code获取name
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        //public static string GetDicNameByCode(int code, string group)
        //{
        //    return ModelHelper.GetDicNameByCode(code, group);
        //}

        /// <summary>
        /// 根据code获取name
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        //public static string GetDicNameByCode(string code, string group)
        //{
        //    return ModelHelper.GetDicNameByCode(code, group);
        //}

        /// <summary>
        /// 根据Value获取name
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string GetNameByGuid(string guid)
        {
            return ModelHelper.GetMappingNameByGuid(guid);
        }

        /// <summary>
        /// 获取所有字典
        /// </summary>
        /// <returns></returns>
        public static List<Mapping> GetDictionarys()
        {
            return ModelHelper.GetMappings();
        }

    }
}
