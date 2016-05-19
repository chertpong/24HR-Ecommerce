using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Models
{
    public class RoleViewModels
    {
        public class CreateRoleViewModel
        {
            public string Name { get; set; }
        }

        public class AddRoleToUserViewModel
        {
            public string RoleName { get; set; }
            public string Username { get; set; }
        }

        public class ShowRolesAndUsersViewModels
        {
            public List<ApplicationUser> Users { get; set; }
            public List<IdentityRole> Roles { get; set; }

        }

        public class RemoveRoleFromUserViewModel
        {
            public string RoleName { get; set; }
            public string Username { get; set; }
        }
    }
}