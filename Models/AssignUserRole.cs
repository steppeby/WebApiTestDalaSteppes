using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AssignUserRole
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
        public AssignUserRole(string email, string roleName)
        {
            Email = email;
            RoleName = roleName;
        }
        public AssignUserRole() { }
    }
}
