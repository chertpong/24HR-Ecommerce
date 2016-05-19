using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}