using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_UserAndGroup")]
    public class UserInGroup
    {
        [Key, Column(Order = 0)]
        public string UserGuid { set; get; }

        [Key, Column(Order = 1)]
        public string UserGroupGuid { set; get; }
    }
}
