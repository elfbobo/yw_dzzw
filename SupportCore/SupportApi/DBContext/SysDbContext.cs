using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Yawei.SupportCore.SupportApi.Entity;

namespace Yawei.SupportCore.SupportApi.DBContext
{
    public class SysDbContext:DbContext
    {
        public SysDbContext(string conStr)
            : base(conStr)
        { 
           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<MenusLicenses> MenusLicenses { get; set; }
        public DbSet<ModelLicenses> ModelLicenses { set; get; }
        public DbSet<Mapping> Mapping { set; get; }
        public DbSet<UserInGroup> UserInGroup { set; get; }
    }
}
