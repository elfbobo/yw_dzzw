using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Yawei.SupportCore.SupportApi.EntityHelper;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_UserGroups")]
    public class Group
    {
        [Key]
        public string Guid { get; set; }
        public string Name { get; set; }
        public string TopGuid { get; set; }
        public string SortNum { get; set; }
        public string Property { get; set; }
        public int Status { set; get; }



        public  List<User> GetUser()
        {
            return ModelHelper.GetGroupUser(Guid);
        }

        public static List<Group> GetGroup()
        {
            return ModelHelper.GetGroup();
        }


        public static System.Xml.XmlDocument GetGroupAndUserXML()
        {
            return ModelHelper.GetGroupAndUserXML();
        }


        public static string GetGroupAndUserJson()
        {
            return ModelHelper.GetGroupAndUserJson();          
           
        }


        /// <summary>
        /// 根据主键获取组
        /// </summary>
        /// <param name="groupGuid"></param>
        /// <returns></returns>
        public static Group GetGroupByGuid(string groupGuid)
        {
            return ModelHelper.GetGroupByGuid(groupGuid);
        }

        /// <summary>
        /// 根据名字获取组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Group> GetGroupByName(string name)
        {
            return ModelHelper.GetGroupByName(name);
        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int AddGroup(Group group)
        {
            return ModelHelper.AddGroup(group);
        }

        /// <summary>
        /// 添加用户到组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int AddGroupUser(string userGuid,string groupGuid)
        {
            return ModelHelper.AddGroupUser(userGuid, groupGuid);
        }

        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int DeleteGroup(Group group)
        {
            return ModelHelper.DeleteGroup(group);
        }

        /// <summary>
        /// 更新组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int UpdateGroup(Group group)
        {
            return ModelHelper.UpdateGroup(group);
        }

    }
}
